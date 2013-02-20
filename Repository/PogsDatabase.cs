using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Pogs.DataModel.Security;
using Pogs.Repository;
using System.Net;

namespace Pogs.DataModel
{
    public class PogsDatabase : IDisposable
    {
        #region Declarations

        SqlConnection _connection;

        DatabaseKeyCache<ClientRecord> _clients = new DatabaseKeyCache<ClientRecord>();
        DatabaseKeyCache<ClientEntry> _entries = new DatabaseKeyCache<ClientEntry>();
        DatabaseKeyCache<EntryTemplate> _templates = new DatabaseKeyCache<EntryTemplate>();
        DatabaseKeyCache<EntryField> _templateFields = new DatabaseKeyCache<EntryField>();
        DatabaseKeyCache<EntryField> _customFields = new DatabaseKeyCache<EntryField>();

        UserKeyCache _users = new UserKeyCache();
        GroupKeyCache _groups = new GroupKeyCache();

        Dictionary<int, TemplateFieldValueRelation> _templateValues = new Dictionary<int, TemplateFieldValueRelation>();

        string _username;

        #endregion Declarations

        #region Properties

        public string ServerName { get; set; }
        public string DatabaseName { get; set; }

        public UserPrincipal CurrentUser { get; private set; }
        public bool Authenticated { get; private set; }

        public IEnumerable<SecurityPrincipal> SecurityPrincipals
        {
            get
            {
                yield return GroupPrincipal.Everyone;

                foreach (var other in _users.Items.Cast<SecurityPrincipal>()
                        .Concat(_groups.Items.Cast<SecurityPrincipal>())
                        .OrderBy(sp => sp.Name))
                {
                    yield return other;
                }
            }
        }

        public SecurityDescriptorCollection DefaultSecurity { get; private set; }

        public IEnumerable<ClientRecord> Clients
        {
            get { return _clients.Items; }
        }

        public IEnumerable<EntryTemplate> Templates
        {
            get { return _templates.Items; }
        }

        internal bool Connected
        {
            get { return _connection != null && _connection.State == System.Data.ConnectionState.Open; }
        }

        #endregion Properties

        #region Events

        public event EventHandler ConnectionUnexpectedlyClosed;

        protected void OnConnectionUnexpectedlyClosed(EventArgs e)
        {
            if (ConnectionUnexpectedlyClosed != null)
            {
                ConnectionUnexpectedlyClosed(this, e);
            }
        }

        public event EventHandler<ClientRecordEventArgs> ClientsAdded;

        protected void OnClientsAdded(ClientRecordEventArgs e)
        {
            if (ClientsAdded != null)
            {
                ClientsAdded(this, e);
            }
        }

        public event EventHandler<ClientRecordEventArgs> ClientsRemoved;

        protected void OnClientsRemoved(ClientRecordEventArgs e)
        {
            if (ClientsRemoved != null)
            {
                ClientsRemoved(this, e);
            }
        }

        public event EventHandler<SecurityPrincipalsEventArgs> PrincipalsAdded;

        protected virtual void OnPrincipalsAdded(SecurityPrincipalsEventArgs e)
        {
            if (PrincipalsAdded != null)
            {
                PrincipalsAdded(this, e);
            }
        }

        public event EventHandler<SecurityPrincipalsEventArgs> PrincipalsRemoved;

        protected virtual void OnPrincipalsRemoved(SecurityPrincipalsEventArgs e)
        {
            if (PrincipalsRemoved != null)
            {
                PrincipalsRemoved(this, e);
            }
        }

        #endregion Events

        #region Constructor

        public PogsDatabase(string username)
        {
            if (String.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");

            _username = username;

            this.DefaultSecurity = new SecurityDescriptorCollection { Owner = this };
        }

        #endregion Constructor

        #region SQL Event Handlers

        private void Connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            if (e.CurrentState == System.Data.ConnectionState.Closed)
            {
                _connection.Dispose();
                _connection = null;
                this.CurrentUser = null;
                OnConnectionUnexpectedlyClosed(EventArgs.Empty);
            }
        }

        #endregion SQL Event Handlers

        #region Public Methods

        public bool Authenticate(string pin)
        {
            if (!Connected)
                Connect();

            //defensive precondition
            if (!this.Connected && pin == null || pin.Length != 4)
                return false;

            var authCommand = new SqlCommand("sp_AuthenticateUser", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            authCommand.Parameters.Add(new SqlParameter("username", _username));
            authCommand.Parameters.Add(new SqlParameter("pin", pin));

            using (var reader = authCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    //first result set is no longer necessary
                }

                reader.NextResult();

                this.Authenticated = false;
                while (reader.Read())
                {
                    bool createdNew;
                    var user = CreateOrUpdateUser(reader, 0, 1, 2, out createdNew);
                    if (createdNew)
                        OnPrincipalsAdded(new SecurityPrincipalsEventArgs(new[] { user }));

                    this.CurrentUser = user;
                    this.Authenticated = true;
                }

                if (!this.Authenticated)
                    return false;

                reader.NextResult();

                //set initial group memberships
                while (reader.Read())
                {
                    bool createdNew;
                    var group = CreateOrUpdateGroup(reader, 0, 1, 2, out createdNew);
                    if (createdNew)
                        OnPrincipalsAdded(new SecurityPrincipalsEventArgs(new[] { group }));

                    if (!group.ContainsMember(this.CurrentUser))
                        group.AddMember(this.CurrentUser);
                }
            }

            return this.Authenticated;
        }

        /// <summary>
        /// Refreshes the list of clients from the database.
        /// </summary>
        public void RefreshClientList()
        {
            if (!this.Connected)
                throw new InvalidOperationException("Cannot get the list of clients because the database is not connected.");
            if (!this.Authenticated)
                throw new InvalidOperationException("Cannot get the list of clients because the current user is not authenticated with the Database.");

            var getListCommand = new SqlCommand("sp_GetClientList", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            getListCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here

            var newClients = new List<ClientRecord>();
            var prevClientIds = _clients.Keys.ToList();

            using (var reader = getListCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    ClientRecord client;
                    int clientDbId = (int)reader[0];

                    if (!_clients.TryGetItem(clientDbId, out client))
                    {
                        client = new ClientRecord();
                        client.Security.Parent = this.DefaultSecurity;
                        _clients.Add(clientDbId, client);
                        newClients.Add(client);
                    }

                    //update the properties of the client, regardless if it existed before
                    client.Name = (string)reader[1];
                    client.ClientId = reader[2] as string;
                }
            }

            //clean up the internal list, and raise events for the UI
            var deletedClientIds = prevClientIds.Except(_clients.Keys);
            if (deletedClientIds.Any())
            {
                var deletedClients = new List<ClientRecord>();
                foreach (var id in deletedClientIds)
                {
                    deletedClients.Add(_clients[id]);
                    _clients.Remove(id);
                }

                OnClientsRemoved(new ClientRecordEventArgs(deletedClients));
            }

            if (newClients.Any())
                OnClientsAdded(new ClientRecordEventArgs(newClients));
        }

        /// <summary>
        /// Populates a ClientRecord's Security and ClientEntries list from the database.
        /// </summary>
        /// <param name="client"></param>
        /// <remarks>
        ///     This method is horrifically long and is due for refactoring.
        /// </remarks>
        public void Refresh(ClientRecord client)
        {
            //preconditions
            if (!this.Connected)
                throw new InvalidOperationException("Cannot get the list of entries for this client because the database is not connected.");
            if (!this.Authenticated)
                throw new InvalidOperationException("Cannot get the list of entries for this client because the current user is not authenticated with the Database.");

            RefreshDefaultSecurity();
            RefreshClientSecurityInternal(client);

            if ((client.Security.Count == 0 && this.DefaultSecurity.CheckIsAllowedView(this.CurrentUser)) ||
                client.Security.CheckIsAllowedView(this.CurrentUser))
            {
                RefreshClientEntriesInternal(client);
            }
            else
            {
                client.RemoveEntries(client.Entries);
            }
        }

        /// <summary>
        /// Updates the Database with any changed client name, id, or entry additions or deletions.
        /// </summary>
        /// <param name="clientRecord"></param>
        public void CommitClient(ClientRecord clientRecord)
        {
            if (clientRecord == null)
                throw new ArgumentNullException("clientRecord");

            int clientId;
            if (!_clients.TryGetKey(clientRecord, out clientId))
                throw new ArgumentException("Cannot commit this client because it does not exist in this database.");

            //update general information about the client
            var renameCommand = new SqlCommand("sp_UpdateClientName", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            renameCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            renameCommand.Parameters.Add(new SqlParameter("clientId", clientId));
            renameCommand.Parameters.Add(new SqlParameter("clientName", clientRecord.Name));
            renameCommand.Parameters.Add(new SqlParameter("clientNumber", clientRecord.ClientId));
            renameCommand.ExecuteNonQuery();

            //add new entries
            var newEntries = clientRecord.Entries.Where(e => !_entries.ContainsItem(e)).ToList();
            if (newEntries.Any())
            {
                var query = new XElement("Entries",
                    from e in newEntries
                    select new XElement("Entry",
                        new XAttribute("client_id", clientId),
                        new XAttribute("template_id", e.Template == null ? 0 : _templates[e.Template]),
                        new XAttribute("icon_id", e.IconIndex),
                        new XAttribute("name", e.Name)));

                var updateCommand = new SqlCommand("sp_AddEntries", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
                updateCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
                updateCommand.Parameters.Add(new SqlParameter("addXml", query.ToString()));

                //assign ids to the new entries
                using (var reader = updateCommand.ExecuteReader())
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        if (i >= newEntries.Count)
                            throw new InvalidOperationException("The database returned more new entires than expected.");

                        int entryId = (int)reader[0];
                        _entries.Add(entryId, newEntries[i]);

                        i++;
                    }

                    if (i < newEntries.Count)
                        throw new InvalidOperationException("The database returned less new entries than expected.");
                }

                foreach (var e in newEntries)
                    AddLogEntry(e, UserLogEntryType.Add);
            }

            //delete
            var deletedEntries = _entries.Items.Where(e => e.Client == clientRecord).Except(clientRecord.Entries).ToList();
            if (deletedEntries.Any())
            {
                var query = new XElement("Entries",
                    from e in deletedEntries
                    select new XElement("Entry",
                        new XAttribute("id", _entries[e])));

                var deleteCommand = new SqlCommand("sp_DeleteEntries", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
                deleteCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
                deleteCommand.Parameters.Add(new SqlParameter("deleteXml", query.ToString()));
                deleteCommand.ExecuteNonQuery();

                //TODO: check count

                foreach (var entry in deletedEntries)
                {
                    //remove the custom field values from memory
                    foreach (var field in entry.CustomFields)
                    {
                        _customFields.Remove(field);
                    }

                    //remove the template field values from memory
                    if (entry.Template != null)
                    {
                        var values = _templateValues
                                .Where(kvp => kvp.Value.ClientEntry == entry)
                                .ToList();

                        foreach (var v in values)
                        {
                            _templateValues.Remove(v.Key);
                        }
                    }

                    //remove the IDs from memory
                    _entries.Remove(entry);
                }

                foreach (var e in deletedEntries)
                    AddLogEntry(e, UserLogEntryType.Delete);
            }
        }

        public void CommitClientSecurity(ClientRecord clientRecord)
        {
            if (clientRecord == null)
                throw new ArgumentNullException("clientRecord");

            int clientId;
            if (!_clients.TryGetKey(clientRecord, out clientId))
                throw new ArgumentException("The client specified does not exist in this database.");
            if (!this.Connected)
                throw new InvalidOperationException("Cannot commit the security settings because the database is not connected.");
            if (!this.Authenticated)
                throw new InvalidOperationException("Cannot commit the security settings because the current user is not authenticated with the Database.");

            //prepare the query in a format understood by sp_UpdateEntryValues
            var query = new XElement("Security");
            foreach (var descriptor in clientRecord.Security)
            {
                XElement descriptorElement = new XElement("Descriptor",
                    new XAttribute("view", descriptor.ViewAllowed),
                    new XAttribute("edit", descriptor.EditingAllowed));

                if (descriptor.SecurityPrincipal is UserPrincipal)
                {
                    int userId;
                    if (!_users.TryGetKey((UserPrincipal)descriptor.SecurityPrincipal, out userId))
                        throw new ArgumentException(String.Format("The user '{0}' does not have a valid user ID.", descriptor.SecurityPrincipal.Name));

                    descriptorElement.Add(new XAttribute("userId", userId));
                }
                else if (descriptor.SecurityPrincipal is GroupPrincipal)
                {
                    if (descriptor.SecurityPrincipal != GroupPrincipal.Everyone)
                    {
                        int groupId;
                        if (!_groups.TryGetKey((GroupPrincipal)descriptor.SecurityPrincipal, out groupId))
                            throw new ArgumentException(String.Format("The group '{0}' does not have a valid user ID.", descriptor.SecurityPrincipal.Name));

                        descriptorElement.Add(new XAttribute("groupId", groupId));
                    }
                }
                else
                {
                    throw new NotSupportedException(String.Format("Security Principals of type '{0}' are not supported by PogsDatabase.", descriptor.SecurityPrincipal.GetType().Name));
                }

                query.Add(descriptorElement);
            }

            //Query the Database
            var updateCommand = new SqlCommand("sp_UpdateClientSecurity", _connection) { CommandType = CommandType.StoredProcedure };
            updateCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            updateCommand.Parameters.Add(new SqlParameter("clientId", clientId));
            updateCommand.Parameters.Add(new SqlParameter("updateXml", query.ToString()));

            updateCommand.ExecuteNonQuery();

            AddLogEntryInternal(UserLogEntryType.Edit, clientRecord, "Changed Security Settings");
        }

        /// <summary>
        /// Updates the Database with any changed data in a sequence of entries (template field values, custom field adds/deletes, notes).
        /// </summary>
        /// <param name="entries"></param>
        public void CommitEntryChanges(IEnumerable<ClientEntry> entries)
        {
            if (!this.Connected)
                throw new InvalidOperationException("Cannot commit the field values because the database is not connected.");
            if (!this.Authenticated)
                throw new InvalidOperationException("Cannot commit the field values because the current user is not authenticated with the Database.");

            if (entries == null)
                return;

            //detect deleted custom fields
            var deletedCustomFields = _customFields.Items.Where(cf => cf.Container == null);

            //prepare the query in a format understood by sp_UpdateEntryValues
            var query = new XElement("Entries",
                from e in entries
                select new XElement("Entry",
                    new XAttribute("id", _entries[e]),
                    new XAttribute("name", e.Name),
                    from f in e.AllFields
                    select new XElement("FieldValue",
                        new XAttribute("id", GetOrCreateFieldValueId(e, f)), DataSecurity.EncryptStringAES(e.GetValue(f))),
                    new XElement("Notes", WebUtility.HtmlEncode(e.Notes ?? String.Empty).Replace(Environment.NewLine, "<br>"))),
                new XElement("DeletedCustomFields",
                    from f in deletedCustomFields
                    select new XElement("DeletedField",
                        new XAttribute("id", _customFields[f]))));

            //Query the Database
            var updateCommand = new SqlCommand("sp_UpdateEntryValues", _connection) { CommandType = CommandType.StoredProcedure };
            updateCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            updateCommand.Parameters.Add(new SqlParameter("updateXml", query.ToString()));

            updateCommand.ExecuteNonQuery();

            foreach (var cf in deletedCustomFields.ToList())
            {
                //TODO: log somehow
                _customFields.Remove(cf);
            }

            foreach (var e in entries)
                AddLogEntry(e, UserLogEntryType.Edit);
        }

        public void AddLogEntry(ClientEntry entry, UserLogEntryType entryType)
        {
            //preconditions
            if (!this.Connected)
                throw new InvalidOperationException("Cannot add the log entry because the database is not connected.");
            if (!this.Authenticated)
                throw new InvalidOperationException("Cannot add the log entry because the current user is not authenticated with the Database.");
            if (entry == null)
                throw new ArgumentNullException("entry");
            if (entryType != UserLogEntryType.Delete && !_entries.ContainsItem(entry))
                throw new ArgumentException("The entry specified is not in this database.");
            if (entryType == UserLogEntryType.Delete && _entries.ContainsItem(entry))
                throw new ArgumentException("A delete log entry cannot be written because the ClientEntry specified is in this database.");

            //prepare the description
            string description = String.Format("{0}: {1}", entry.Client.Name, entry.Name);
            var client = entry.Client;

            AddLogEntryInternal(entryType, client, description);
        }

        public void AddNewClient(ClientRecord clientRecord)
        {
            if (!this.Connected)
                throw new InvalidOperationException("Cannot add the client because the database is not connected.");
            if (!this.Authenticated)
                throw new InvalidOperationException("Cannot add the client because the current user is not authenticated with the Database.");
            if (clientRecord == null)
                throw new ArgumentNullException("clientRecord");
            if (_clients.ContainsItem(clientRecord))
                throw new ArgumentException("Cannot add this client because it already exists in this database.");

            //execute the stored procedure
            var addCommand = new SqlCommand("sp_AddClient", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            addCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            addCommand.Parameters.Add(new SqlParameter("client_name", clientRecord.Name));
            addCommand.Parameters.Add(new SqlParameter("client_id", clientRecord.ClientId));

            //assign the IDs
            int newId = (int)addCommand.ExecuteScalar();
            _clients.Add(newId, clientRecord);
            clientRecord.Security.Parent = this.DefaultSecurity;

            //raise events
            OnClientsAdded(new ClientRecordEventArgs(clientRecord));

            AddLogEntryInternal(UserLogEntryType.Add, clientRecord, String.Format("Added new client: {0}", clientRecord.Name));
        }

        public void PurgeClient(ClientRecord clientRecord)
        {
            if (!this.Connected)
                throw new InvalidOperationException("Cannot purge the client because the database is not connected.");
            if (!this.Authenticated)
                throw new InvalidOperationException("Cannot purge the client because the current user is not authenticated with the Database.");
            if (clientRecord == null)
                throw new ArgumentNullException("clientRecord");

            int id;
            if (!_clients.TryGetKey(clientRecord, out id))
                throw new ArgumentException("Cannot purge this client because it does not exist in this database.");

            //delete entries and associated values (takes care of both memory and DB)
            clientRecord.RemoveEntries(clientRecord.Entries.ToList());
            CommitClient(clientRecord);

            //delete client in DB
            var purgeCommand = new SqlCommand("sp_PurgeClient", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            purgeCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            purgeCommand.Parameters.Add(new SqlParameter("client_id", id));
            purgeCommand.ExecuteNonQuery();

            //delete client in memory
            _clients.Remove(clientRecord);

            OnClientsRemoved(new ClientRecordEventArgs(clientRecord));

            AddLogEntryInternal(UserLogEntryType.Delete, null, String.Format("Purged client: {0}", clientRecord.Name));
        }

        public void Refresh(SecurityDescriptorCollection securityDescriptorCollection)
        {
            if (securityDescriptorCollection == null)
                throw new ArgumentNullException("securityDescriptorCollection");
            if (securityDescriptorCollection.Owner == null)
                throw new ArgumentException("The SecurityDescriptorCollection specified does not have an owner.");

            if (securityDescriptorCollection.Owner is PogsDatabase)
                RefreshDefaultSecurity();
            else if (securityDescriptorCollection.Owner is ClientRecord)
                RefreshClientSecurityInternal((ClientRecord)securityDescriptorCollection.Owner);
            else
                throw new NotSupportedException(String.Format("PogsDatabase cannot refresh a SecurityDescriptorCollection of type {0}.", securityDescriptorCollection.GetType().Name));
        }

        private void RefreshDefaultSecurity()
        {
            if (!this.Connected)
                throw new InvalidOperationException("Cannot refresh the default security because the database is not connected.");
            if (!this.Authenticated)
                throw new InvalidOperationException("Cannot refresh the default security because the current user is not authenticated with the Database.");

            var getDefaultSecurityCommand = new SqlCommand("sp_GetDefaultSecurity", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            getDefaultSecurityCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here

            using (var reader = getDefaultSecurityCommand.ExecuteReader())
            {
                this.DefaultSecurity.Clear();

                while (reader.Read())
                {
                    int userId = reader.IsDBNull(0) ? -1 : (int)reader[0];
                    int groupId = reader.IsDBNull(1) ? -1 : (int)reader[1];

                    if (userId != -1 && groupId != -1)
                        throw new InvalidOperationException("An invalid default security record was received from the Database.");

                    SecurityPrincipal target;
                    bool createdNew = false;
                    if (userId != -1)
                        target = CreateOrUpdateUser(reader, 0, -1, -1, out createdNew);
                    else if (groupId != -1)
                        target = CreateOrUpdateGroup(reader, 1, -1, -1, out createdNew);
                    else
                        target = GroupPrincipal.Everyone;   //both null means everyone

                    if (createdNew)
                        OnPrincipalsAdded(new SecurityPrincipalsEventArgs(target));

                    this.DefaultSecurity.Add(new SecurityDescriptor(target, (bool)reader[3], (bool)reader[2]));
                }
            }
        }

        public void CommitDefaultSecurity()
        {
            if (!this.Connected)
                throw new InvalidOperationException("Cannot save the default security because the database is not connected.");
            if (!this.Authenticated)
                throw new InvalidOperationException("Cannot save the default security because the current user is not authenticated with the Database.");

            //prepare the query in a format understood by sp_UpdateEntryValues
            var query = new XElement("Security");
            foreach (var descriptor in this.DefaultSecurity)
            {
                XElement descriptorElement = new XElement("Descriptor",
                    new XAttribute("view", descriptor.ViewAllowed),
                    new XAttribute("edit", descriptor.EditingAllowed));

                if (descriptor.SecurityPrincipal is UserPrincipal)
                {
                    int userId;
                    if (!_users.TryGetKey((UserPrincipal)descriptor.SecurityPrincipal, out userId))
                        throw new ArgumentException(String.Format("The user '{0}' does not have a valid user ID.", descriptor.SecurityPrincipal.Name));

                    descriptorElement.Add(new XAttribute("userId", userId));
                }
                else if (descriptor.SecurityPrincipal is GroupPrincipal)
                {
                    if (descriptor.SecurityPrincipal != GroupPrincipal.Everyone)
                    {
                        int groupId;
                        if (!_groups.TryGetKey((GroupPrincipal)descriptor.SecurityPrincipal, out groupId))
                            throw new ArgumentException(String.Format("The group '{0}' does not have a valid user ID.", descriptor.SecurityPrincipal.Name));

                        descriptorElement.Add(new XAttribute("groupId", groupId));
                    }
                }
                else
                {
                    throw new NotSupportedException(String.Format("Security Principals of type '{0}' are not supported by PogsDatabase.", descriptor.SecurityPrincipal.GetType().Name));
                }

                query.Add(descriptorElement);
            }

            //Query the Database
            var updateCommand = new SqlCommand("sp_UpdateDefaultSecurity", _connection) { CommandType = CommandType.StoredProcedure };
            updateCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            updateCommand.Parameters.Add(new SqlParameter("updateXml", query.ToString()));

            updateCommand.ExecuteNonQuery();

            AddLogEntryInternal(UserLogEntryType.Edit, null, "Changed Default Security Settings");
        }

        public void RefreshTemplates()
        {
            if (!this.Connected)
                throw new InvalidOperationException("Cannot refresh the template list because the database is not connected.");
            if (!this.Authenticated)
                throw new InvalidOperationException("Cannot refresh the template list because the current user is not authenticated with the Database.");

            var getListCommand = new SqlCommand("sp_GetAllTemplates", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            getListCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here

            using (var reader = getListCommand.ExecuteReader())
            {
                HashSet<int> templateIdsInMemory = new HashSet<int>(_templates.Keys);

                while (reader.Read())
                {
                    int templateId = (int)reader[0];
                    if (templateIdsInMemory.Contains(templateId))
                        templateIdsInMemory.Remove(templateId);

                    EntryTemplate template;
                    if (!_templates.TryGetItem(templateId, out template))
                    {
                        template = new EntryTemplate();
                        _templates.Add(templateId, template);
                    }

                    //update all fields
                    template.Name = reader[1] as string;
                    template.IconIndex = (int)reader[2];
                    template.AllowCustomFields = (bool)reader[3];
                }

                //delete templates that were not returned
                foreach (var id in templateIdsInMemory)
                {
                    _templates.Remove(id);
                }

                reader.NextResult();

                HashSet<int> templateFieldIdsInMemory = new HashSet<int>(_templateFields.Keys);

                while (reader.Read())
                {
                    int templateFieldId = (int)reader[0];
                    if (templateFieldIdsInMemory.Contains(templateFieldId))
                        templateFieldIdsInMemory.Remove(templateFieldId);

                    EntryField templateField;
                    if (!_templateFields.TryGetItem(templateFieldId, out templateField))
                    {
                        //add the template field to the appropriate template
                        int templateId = (int)reader[1];
                        EntryTemplate template;
                        if (!_templates.TryGetItem(templateId, out template))
                            throw new InvalidOperationException("A template field was returned for a template that is not defined in the database.");

                        templateField = new EntryField();
                        template.AddField(templateField);

                        _templateFields.Add(templateFieldId, templateField);
                    }

                    templateField.Name = reader[2] as string;
                    templateField.EntryType = (EntryFieldType)reader[3];
                    templateField.DefaultValue = reader[4] as string;
                }

                //remove fields that were not returned
                foreach (int id in templateFieldIdsInMemory)
                {
                    _templateFields.Remove(id);
                }
            }
        }

        public void AddTemplate(EntryTemplate template)
        {
            if (template == null)
                throw new ArgumentNullException("template");
            if (_templates.ContainsItem(template))
                throw new ArgumentException("This Database already contains this template.");

            var fields = template.Fields.ToList();

            var query = new XElement("Template",
                new XAttribute("name", template.Name),
                new XAttribute("icon_id", template.IconIndex),
                new XAttribute("allow_custom_fields", template.AllowCustomFields),
                new XElement("Fields",
                    from f in fields
                    select new XElement("Field",
                        new XAttribute("name", f.Name),
                        new XAttribute("type", (int)f.EntryType),
                        new XAttribute("default_value", f.DefaultValue ?? String.Empty))));

            //Query the Database
            var addCommand = new SqlCommand("sp_AddTemplate", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            addCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            addCommand.Parameters.Add(new SqlParameter("addXml", query.ToString()));

            using (var reader = addCommand.ExecuteReader())
            {
                //assign IDs to the template
                int templateId = -1;
                while (reader.Read())
                {
                    templateId = (int)reader[0];
                    break;
                }
                if (templateId == -1)
                    throw new InvalidOperationException("No template ID was returned by the database.");

                _templates.Add(templateId, template);

                reader.NextResult();

                //assign IDs to the template fields
                int fieldIndex = 0;
                while (reader.Read())
                {
                    if (fieldIndex > fields.Count - 1)
                        throw new InvalidOperationException("More template field IDs were returned from the database than expected.");

                    int fieldId = (int)reader[0];
                    _templateFields.Add(fieldId, fields[fieldIndex]);
                    fieldIndex++;
                }
            }
        }

        public void RemoveTemplate(EntryTemplate template)
        {
            if (template == null)
                throw new ArgumentNullException("template");
            if (!_templates.ContainsItem(template))
                throw new ArgumentException("This Database does not contain this template.");

            //find any entries in memory that use this template and convert them to custom fields, calls the db also
            foreach (var field in template.Fields.ToList())
            {
                DemoteTemplateField(template, field);
            }

            var affectedEntries = _entries.Items.Where(e => e.Template == template).ToList();
            foreach (var entry in affectedEntries)
            {
                entry.Template = null;
            }
            CommitEntryChanges(affectedEntries);

            int templateId = _templates[template];

            //call database
            var purgeCommand = new SqlCommand("sp_DeleteTemplate", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            purgeCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            purgeCommand.Parameters.Add(new SqlParameter("template_id", templateId));
            purgeCommand.ExecuteNonQuery();

            _templates.Remove(template);
        }

        /// <summary>
        /// Saves a template and its field definitions to the database from memory.
        /// </summary>
        /// <param name="template"></param>
        public void CommitTemplate(EntryTemplate template)
        {
            if (!this.Connected)
                throw new InvalidOperationException("Cannot commit the template changes because the database is not connected.");
            if (!this.Authenticated)
                throw new InvalidOperationException("Cannot commit the template changes because the current user is not authenticated with the Database.");

            //create the query first
            var query = new XElement("Template",
                new XAttribute("id", _templates[template]),
                new XAttribute("name", template.Name),
                new XAttribute("icon_id", template.IconIndex),
                new XAttribute("allow_custom_fields", template.AllowCustomFields),
                new XElement("Fields",
                    from f in template.Fields
                    select new XElement("Field",
                        new XAttribute("id", _templateFields.ContainsItem(f) ? _templateFields[f] : -1),
                        new XAttribute("name", f.Name),
                        new XAttribute("type", (int)f.EntryType),
                        new XAttribute("default_value", f.DefaultValue ?? String.Empty))));

            //demote any fields that were deleted second
            var deletedFields = _templateFields.Items.Where(f => f.Container == template && !template.Fields.Contains(f)).ToList();
            foreach (var field in deletedFields)
            {
                DemoteTemplateField(template, field);
            }

            //Query the Database
            var updateCommand = new SqlCommand("sp_UpdateTemplate", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            updateCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            updateCommand.Parameters.Add(new SqlParameter("updateXml", query.ToString()));

            //assign IDs from the -1 Fields
            using (var reader = updateCommand.ExecuteReader())
            {
                var unassigned = template.Fields.Where(f => !_templateFields.ContainsItem(f)).ToList();
                int index = 0;
                while (reader.Read())
                {
                    if (index > (unassigned.Count - 1))
                        throw new InvalidOperationException("The database returned new IDs that were not expected.");

                    int id = (int)reader[0];
                    _templateFields.Add(id, unassigned[index]);

                    index++;
                }

                if (index != unassigned.Count)
                    throw new InvalidOperationException("The database did not return new IDs for all the new template fields.");
            }
        }

        public void RefreshSecurityPrincipals()
        {
            if (!this.Connected)
                throw new InvalidOperationException("Cannot get the list of clients because the database is not connected.");
            if (!this.Authenticated)
                throw new InvalidOperationException("Cannot get the list of clients because the current user is not authenticated with the Database.");

            var getPrincipalsCommand = new SqlCommand("sp_GetSecurityPrincipals", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            getPrincipalsCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here

            var newPrincipals = new List<SecurityPrincipal>();
            var prevUserIds = _users.Keys.ToList();

            using (var reader = getPrincipalsCommand.ExecuteReader())
            {
                //users
                while (reader.Read())
                {
                    bool createdNew;
                    var user = CreateOrUpdateUser(reader, 0, 1, 2, out createdNew);
                    if (createdNew)
                        newPrincipals.Add(user);
                }

                reader.NextResult();

                //groups
                while (reader.Read())
                {
                    bool createdNew;
                    var group = CreateOrUpdateGroup(reader, 0, 1, 2, out createdNew);
                    if (createdNew)
                        newPrincipals.Add(group);
                }

                //group members
                List<GroupMembership> memberships = new List<GroupMembership>();
                while (reader.Read())
                {
                    memberships.Add(new GroupMembership { UserId = (int)reader[0], GroupId = (int)reader[1] });
                }

                foreach (var membership in memberships.GroupBy(m => m.GroupId))
                {
                    GroupPrincipal group;
                    if (!_groups.TryGetItem(membership.Key, out group))
                        throw new InvalidOperationException("An invalid group membership was returned by the server.");

                    HashSet<UserPrincipal> current = new HashSet<UserPrincipal>();

                    foreach (var userId in membership.Select(m => m.UserId))
                    {
                        UserPrincipal user;
                        if (!_users.TryGetItem(userId, out user))
                            throw new InvalidOperationException("An invalid group member was returned by the server.");

                        if (current.Contains(user))
                            current.Remove(user);
                        else
                            group.AddMember(user);
                    }

                    foreach (var orphan in current)
                        group.RemoveMember(orphan);
                }
            }

            //clean up the internal list, and raise events for the UI
            var deletedUserIds = prevUserIds.Except(_users.Keys);
            if (deletedUserIds.Any())
            {
                var deletedUsers = new List<SecurityPrincipal>();
                foreach (var id in deletedUserIds)
                {
                    deletedUsers.Add(_users[id]);
                    _users.Remove(id);
                }

                OnPrincipalsRemoved(new SecurityPrincipalsEventArgs(deletedUsers));
            }

            if (newPrincipals.Any())
                OnPrincipalsAdded(new SecurityPrincipalsEventArgs(newPrincipals));

            _users.SetCheckPoint();
            _groups.SetCheckPoint();
        }

        public void CommitSecurityPrincipals(IEnumerable<SecurityPrincipal> allPrincipals)
        {
            if (!this.Connected)
                throw new InvalidOperationException("Cannot save users or groups because the database is not connected.");
            if (!this.Authenticated)
                throw new InvalidOperationException("Cannot save users or groups because the current user is not authenticated with the Database.");

            CommitUserChangesInternal(allPrincipals.OfType<UserPrincipal>());
            CommitGroupChangesInternal(allPrincipals.OfType<GroupPrincipal>());
        }

        public void ResetUserPin(UserPrincipal userPrincipal, string pin)
        {
            var updateCommand = new SqlCommand("sp_UpdatePin", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            updateCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            updateCommand.Parameters.Add(new SqlParameter("userId", _users[userPrincipal]));
            updateCommand.Parameters.Add(new SqlParameter("pin", pin));
            updateCommand.ExecuteNonQuery();
        }

        #endregion Public Methods

        #region Private Methods

        private void RefreshClientEntriesInternal(ClientRecord client)
        {
            //preparation
            int clientId = _clients[client];
            if (!_clients.TryGetKey(client, out clientId))
                throw new ArgumentException("The specified client is not in this database.");

            var newEntries = new List<ClientEntry>();
            var prevEntryIds = client.Entries.Select(e => _entries[e]).ToList();

            //Query the Database
            var getListCommand = new SqlCommand("sp_GetClientEntries", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            getListCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            getListCommand.Parameters.Add(new SqlParameter("client_id", clientId));

            using (var reader = getListCommand.ExecuteReader())
            {
                //result set 1: entry list for the specified client
                while (reader.Read())
                {
                    ClientEntry entry;
                    int clientEntryDbId = (int)reader[0];

                    //index the entry if it is new
                    if (!_entries.TryGetItem(clientEntryDbId, out entry))
                    {
                        entry = new ClientEntry();
                        _entries.Add(clientEntryDbId, entry);
                        newEntries.Add(entry);
                    }

                    //update the properties of the entry, regardless if it existed before
                    entry.Name = (string)reader[2];
                    entry.IconIndex = (int)reader[3];
                    entry.Notes = WebUtility.HtmlDecode((reader[5] as string ?? String.Empty).Replace("<br>", Environment.NewLine));

                    //set the template, it will get updated later if it doesn't exist yet
                    //we index the new template right away
                    EntryTemplate template;
                    int templateId = (reader[4] == DBNull.Value) ? -1 : (int)reader[4];
                    if (templateId == -1)
                    {
                        template = null;    //orphaned record
                    }
                    else if (!_templates.TryGetItem(templateId, out template))
                    {
                        template = new EntryTemplate();
                        _templates.Add(templateId, template);
                    }
                    entry.Template = template;
                }

                //result set 2: template definition
                reader.NextResult();
                while (reader.Read())
                {
                    EntryTemplate template;
                    int templateDbId = (int)reader[0];

                    if (!_templates.TryGetItem(templateDbId, out template))
                        throw new InvalidOperationException("A template was returned that was not part of the first result set.");

                    //update the properties
                    template.Name = reader[1] as string;
                    template.IconIndex = (int)reader[2];
                    template.AllowCustomFields = (bool)reader[3];
                }

                //result set 3: fields for the previous templates
                reader.NextResult();
                while (reader.Read())
                {
                    EntryField templateField;
                    EntryTemplate template;
                    int templateFieldId = (int)reader[0];
                    int templateId = (int)reader[1];
                    if (!_templates.TryGetItem(templateId, out template))
                        throw new InvalidOperationException("A template field was returned that belongs to a template that was not returned in the previous result set.");

                    if (!_templateFields.TryGetItem(templateFieldId, out templateField))
                    {
                        templateField = new EntryField();
                        _templateFields.Add(templateFieldId, templateField);
                    }

                    if (!template.Fields.Contains(templateField))
                        template.AddField(templateField);

                    //update the properties
                    templateField.Name = reader[2] as string;
                    templateField.EntryType = (EntryFieldType)reader[3];
                    templateField.DefaultValue = reader[4] as string;
                }
                //TODO: remove any fields that were not returned

                //result set 4: field values
                reader.NextResult();
                while (reader.Read())
                {
                    int fieldValueId = (int)reader[0];
                    int entryId = (int)reader[1];

                    ClientEntry entry;
                    if (!_entries.TryGetItem(entryId, out entry))
                        throw new InvalidOperationException("A ClientEntry was returned that was not defined in a previous result set.");

                    int templateFieldId = (reader[2] == DBNull.Value) ? -1 : (int)reader[2];
                    string entryValue = reader[4] as string;

                    //indexing of fields values:
                    //  template field values are indexed by TFVR objects in the _templateFieldsById dictionary
                    //  custom field values are indexed by just the EntryField object in the _customFieldsById dictionary
                    if (templateFieldId == -1)
                    {
                        //custom field
                        string fieldName = reader[3] as string;
                        EntryField customField;
                        if (!_customFields.TryGetItem(fieldValueId, out customField))
                        {
                            customField = new EntryField() { Name = fieldName };    //todo: type
                            entry.AddCustomField(customField);
                            _customFields.Add(fieldValueId, customField);
                        }

                        //update the name and value
                        customField.Name = fieldName;
                        entry.SetValue(customField, DataSecurity.DecryptStringAES(entryValue));
                    }
                    else
                    {
                        //find the template EntryField by ID
                        EntryField templateField;
                        if (_templateFields.TryGetItem(templateFieldId, out templateField))
                        {
                            //save the ID of the field value for saving later
                            if (!_templateValues.Values.Any(tfvr => tfvr.ClientEntry == entry && tfvr.TemplateField == templateField))
                                _templateValues.Add(fieldValueId, new TemplateFieldValueRelation() { ClientEntry = entry, TemplateField = templateField });

                            entry.SetValue(templateField, DataSecurity.DecryptStringAES(entryValue));
                        }
                        else
                        {
                            throw new InvalidOperationException("A template field was returned that cannot be matched to an existing template.");
                        }
                    }
                }
                //TODO: remove any fields that were not returned
            }

            //clean up the internal list, and raise events for the UI
            var deletedEntryIds = prevEntryIds.Except(_entries.Keys).ToList();
            if (deletedEntryIds.Any())
            {
                var deletedEntries = new List<ClientEntry>();
                foreach (var id in deletedEntryIds)
                {
                    var entry = _entries[id];
                    deletedEntries.Add(entry);
                    _entries.Remove(entry);
                }

                client.RemoveEntries(deletedEntries);
            }

            if (newEntries.Any())
                client.AddEntries(newEntries);
        }

        private void RefreshClientSecurityInternal(ClientRecord client)
        {
            var getDefaultSecurityCommand = new SqlCommand("sp_GetClientSecurity", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            getDefaultSecurityCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            getDefaultSecurityCommand.Parameters.Add(new SqlParameter("clientId", _clients[client]));

            using (var reader = getDefaultSecurityCommand.ExecuteReader())
            {
                client.Security.Clear();

                while (reader.Read())
                {
                    int userId = reader.IsDBNull(0) ? -1 : (int)reader[0];
                    int groupId = reader.IsDBNull(1) ? -1 : (int)reader[1];

                    if (userId != -1 && groupId != -1)
                        throw new InvalidOperationException("An invalid default security record was received from the Database.");

                    SecurityPrincipal target;
                    bool createdNew = false;
                    if (userId != -1)
                        target = CreateOrUpdateUser(reader, 0, -1, -1, out createdNew);
                    else if (groupId != -1)
                        target = CreateOrUpdateGroup(reader, 1, -1, -1, out createdNew);
                    else
                        target = GroupPrincipal.Everyone;   //both null means everyone

                    if (createdNew)
                        OnPrincipalsAdded(new SecurityPrincipalsEventArgs(target));

                    client.Security.Add(new SecurityDescriptor(target, (bool)reader[3], (bool)reader[2]));
                }
            }
        }

        private void AddLogEntryInternal(UserLogEntryType entryType, ClientRecord client, string description)
        {
            //send the command
            var addLogEntryCommand = new SqlCommand("sp_AddLogEntry", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            addLogEntryCommand.Parameters.Add(new SqlParameter("username", this.CurrentUser.Name));
            addLogEntryCommand.Parameters.Add(new SqlParameter("actiontype", (int)entryType));
            addLogEntryCommand.Parameters.Add(new SqlParameter("clientid", client == null ? -1 : _clients[client]));
            addLogEntryCommand.Parameters.Add(new SqlParameter("description", description));

            addLogEntryCommand.ExecuteNonQuery();
        }

        private string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = this.ServerName,
                InitialCatalog = this.DatabaseName,
                IntegratedSecurity = true
            };

            return builder.ToString();
        }

        private void Connect()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                throw new InvalidOperationException("Cannot connect because the connection is already open.");

            _connection = new SqlConnection(GetConnectionString());
            _connection.Open();

            _connection.StateChange += new System.Data.StateChangeEventHandler(Connection_StateChange);
        }

        private void Disconnect()
        {
            if (_connection != null)
            {
                _connection.StateChange -= Connection_StateChange;
                _connection.Dispose();
                _connection = null;
            }

            this.CurrentUser = null;
            this.Authenticated = false;
        }

        private int GetOrCreateFieldValueId(ClientEntry entry, EntryField field)
        {
            int match = -1;

            bool isTemplate = field.Container is EntryTemplate;
            if (isTemplate)
            {
                var matches = _templateValues
                    .Where(kvp => kvp.Value.ClientEntry == entry && kvp.Value.TemplateField == field)
                    .ToList();

                if (matches.Count() == 1)
                    match = matches.First().Key;
                else if (matches.Count() > 1)
                    throw new InvalidOperationException("Database inconsistency detected.  Restart Pogs before continuting.");
            }
            else if (field.Container is ClientEntry && _customFields.ContainsItem(field))
            {
                match = _customFields[field];
            }

            if (match != -1)
            {
                return match;
            }
            else
            {
                var addCommand = new SqlCommand("sp_AddFieldValue", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
                addCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
                addCommand.Parameters.Add(new SqlParameter("entry_id", _entries[entry]));
                addCommand.Parameters.Add(new SqlParameter("template_field_id", isTemplate ? (object)_templateFields[field] : DBNull.Value));
                addCommand.Parameters.Add(new SqlParameter("name", isTemplate ? (object)DBNull.Value : (object)field.Name));
                addCommand.Parameters.Add(new SqlParameter("value", DBNull.Value));

                //assign the IDs
                int newId = (int)addCommand.ExecuteScalar();
                if (isTemplate)
                    _templateValues.Add(newId, new TemplateFieldValueRelation() { ClientEntry = entry, TemplateField = field });
                else
                    _customFields.Add(newId, field);

                return newId;
            }
        }

        private void DemoteTemplateField(EntryTemplate template, EntryField field)
        {
            foreach (var entry in _entries.Items.Where(e => e.Template == template))
            {
                //transfer the value to the custom field if applicable
                var relation = _templateValues.FirstOrDefault(tfvr => tfvr.Value.ClientEntry == entry && tfvr.Value.TemplateField == field);
                if (relation.Value != null)
                {
                    var newField = new EntryField() { Name = field.Name, EntryType = field.EntryType };
                    entry.AddCustomField(newField);

                    var value = entry.GetOrphanedValue(field);
                    entry.SetValue(newField, DataSecurity.DecryptStringAES(value));

                    _templateValues.Remove(relation.Key);
                    _customFields.Add(relation.Key, newField);
                }
            }

            //call the DB to do the same as above
            int fieldId = _templateFields[field];
            var purgeCommand = new SqlCommand("sp_DeleteTemplateField", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            purgeCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            purgeCommand.Parameters.Add(new SqlParameter("template_field_id", fieldId));
            purgeCommand.ExecuteNonQuery();

            _templateFields.Remove(fieldId);
        }

        private void CommitGroupChangesInternal(IEnumerable<GroupPrincipal> groups)
        {
            var existingGroups = new HashSet<GroupPrincipal>(_groups.Items);
            var updatedGroups = new List<GroupPrincipal>();
            var newGroups = new List<GroupPrincipal>();

            foreach (var group in groups)
            {
                if (_groups.ContainsItem(group))
                {
                    //update
                    if (_groups.HasChanged(group))
                        updatedGroups.Add(group);

                    //update
                    if (existingGroups.Contains(group))
                        existingGroups.Remove(group);
                }
                else
                {
                    newGroups.Add(group);
                }
            }

            AddGroupsInternal(newGroups);
            UpdateGroupsInternal(updatedGroups);
            DeleteGroupsInternal(existingGroups); //orphaned

            _groups.SetCheckPoint();
        }

        private void CommitUserChangesInternal(IEnumerable<UserPrincipal> users)
        {
            var existingUsers = new HashSet<UserPrincipal>(_users.Items);
            var updatedUsers = new List<UserPrincipal>();
            var newUsers = new List<UserPrincipal>();

            foreach (var user in users)
            {
                if (_users.ContainsItem(user))
                {
                    //update
                    if (_users.HasChanged(user))
                        updatedUsers.Add(user);

                    if (existingUsers.Contains(user))
                        existingUsers.Remove(user);
                }
                else
                {
                    newUsers.Add(user);
                }
            }

            AddUsersInternal(newUsers);
            UpdateUsersInternal(updatedUsers);
            DeleteUsersInternal(existingUsers); //orphaned

            _users.SetCheckPoint();
        }

        public UserPrincipal CreateUser(string username, string pin)
        {
            //update general information about the client
            var updateCommand = new SqlCommand("sp_CreateUser", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            updateCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            updateCommand.Parameters.Add(new SqlParameter("username", username));
            updateCommand.Parameters.Add(new SqlParameter("isAdmin", 0));
            updateCommand.Parameters.Add(new SqlParameter("pin", pin));
            int newIdObj = (int)updateCommand.ExecuteScalar();

            var newUser = new UserPrincipal { Name = username };
            _users.Add(newIdObj, newUser);
            return newUser;
        }

        private void AddGroupsInternal(IEnumerable<GroupPrincipal> newGroups)
        {
            if (!newGroups.Any())
                return;

            foreach (var groupPrincipal in newGroups)
            {
                //update general information about the client
                var updateCommand = new SqlCommand("sp_CreateGroup", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
                updateCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
                updateCommand.Parameters.Add(new SqlParameter("groupName", groupPrincipal.Name));
                updateCommand.Parameters.Add(new SqlParameter("isAdmin", groupPrincipal.IsAdmin ? 1 : 0));
                int newId = (int)updateCommand.ExecuteScalar();

                _groups.Add(newId, groupPrincipal);

                UpdateGroupInternal(groupPrincipal);
            }
        }

        private void UpdateGroupInternal(GroupPrincipal groupPrincipal)
        {
            var members = new XElement("Members",
                from m in groupPrincipal.Members.OfType<UserPrincipal>()
                select new XElement("Member", new XAttribute("userId", _users[m])),
                from g in groupPrincipal.Members.OfType<GroupPrincipal>()
                select new XElement("Member", new XAttribute("groupId", _groups[g])));

            //update general information about the client
            var updateCommand = new SqlCommand("sp_UpdateGroup", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
            updateCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
            updateCommand.Parameters.Add(new SqlParameter("groupId", _groups[groupPrincipal]));
            updateCommand.Parameters.Add(new SqlParameter("groupName", groupPrincipal.Name));
            updateCommand.Parameters.Add(new SqlParameter("isAdmin", groupPrincipal.IsAdmin ? 1 : 0));
            updateCommand.Parameters.Add(new SqlParameter("members", members.ToString()));
            updateCommand.ExecuteNonQuery();

            _groups.SetItemClean(groupPrincipal);
        }

        private void UpdateGroupsInternal(IEnumerable<GroupPrincipal> updatedGroups)
        {
            if (!updatedGroups.Any())
                return;

            foreach (var group in updatedGroups)
                UpdateGroupInternal(group);
        }

        private void DeleteGroupsInternal(IEnumerable<GroupPrincipal> existingGroups)
        {
            if (!existingGroups.Any())
                return;

            foreach (var group in existingGroups)
            {
                //update general information about the client
                var updateCommand = new SqlCommand("sp_DeleteGroup", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
                updateCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
                updateCommand.Parameters.Add(new SqlParameter("groupId", _groups[group]));
                updateCommand.ExecuteNonQuery();

                _groups.Remove(group);
            }
        }

        private void DeleteUsersInternal(IEnumerable<UserPrincipal> existingUsers)
        {
            if (!existingUsers.Any())
                return;

            foreach (var user in existingUsers)
            {
                //update general information about the client
                var updateCommand = new SqlCommand("sp_DeleteUser", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
                updateCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
                updateCommand.Parameters.Add(new SqlParameter("userId", _users[user]));
                updateCommand.ExecuteNonQuery();

                _users.Remove(user);
            }
        }

        private void UpdateUsersInternal(IEnumerable<UserPrincipal> updatedUsers)
        {
            if (!updatedUsers.Any())
                return;

            foreach (var user in updatedUsers)
            {
                //update general information about the client
                var updateCommand = new SqlCommand("sp_UpdateUser", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
                updateCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
                updateCommand.Parameters.Add(new SqlParameter("userId", _users[user]));
                updateCommand.Parameters.Add(new SqlParameter("username", user.Name));
                updateCommand.Parameters.Add(new SqlParameter("isAdmin", user.IsAdmin ? 1 : 0));
                updateCommand.ExecuteNonQuery();

                _users.SetItemClean(user);
            }
        }

        private void AddUsersInternal(IEnumerable<UserPrincipal> newUsers)
        {
            if (!newUsers.Any())
                return;

            foreach (var user in newUsers)
            {
                //update general information about the client
                var updateCommand = new SqlCommand("sp_CreateUser", _connection) { CommandType = System.Data.CommandType.StoredProcedure };
                updateCommand.Parameters.Add(new SqlParameter("token", "null_token"));    //TODO: token will go here
                updateCommand.Parameters.Add(new SqlParameter("username", user.Name));
                updateCommand.Parameters.Add(new SqlParameter("isAdmin", user.IsAdmin ? 1 : 0));
                int newIdObj = (int)updateCommand.ExecuteScalar();

                _users.Add(newIdObj, user);
            }
        }

        private GroupPrincipal CreateOrUpdateGroup(SqlDataReader reader, int idIndex, int nameIndex, int adminIndex, out bool createdNew)
        {
            int id = (int)reader[idIndex];

            GroupPrincipal group;
            if (!_groups.TryGetItem(id, out group))
            {
                group = new GroupPrincipal();
                _groups.Add(id, group);
                createdNew = true;
            }
            else
            {
                createdNew = false;
            }

            if (nameIndex != -1)
                group.Name = (string)reader[nameIndex];
            if (adminIndex != -1)
                group.IsAdmin = (bool)reader[adminIndex];

            return group;
        }

        private UserPrincipal CreateOrUpdateUser(SqlDataReader reader, int idIndex, int nameIndex, int adminIndex, out bool createdNew)
        {
            UserPrincipal user;
            int userDbId = (int)reader[idIndex];

            if (!_users.TryGetItem(userDbId, out user))
            {
                user = new UserPrincipal();
                _users.Add(userDbId, user);
                createdNew = true;
            }
            else
            {
                createdNew = false;
            }

            //update the properties of the user, regardless if it existed before
            if (nameIndex != -1)
                user.Name = (string)reader[nameIndex];
            if (adminIndex != -1)
                user.IsAdmin = (bool)reader[adminIndex];

            return user;
        }

        #endregion Private Methods

        #region IDisposable Members

        public void Dispose()
        {
            Disconnect();
        }

        #endregion IDisposable Members

        #region Nested Classes

        private class TemplateFieldValueRelation
        {
            internal ClientEntry ClientEntry { get; set; }
            internal EntryField TemplateField { get; set; }
        }

        internal class GroupMembership
        {
            internal int GroupId { get; set; }
            internal int UserId { get; set; }
        }

        #endregion Nested Classes
    }
}