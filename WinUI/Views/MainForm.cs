using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Pogs.DataModel;
using Pogs.ViewModels;
using Pogs.VisualModel;
using Pogs.WinUI.Utilities;

namespace Pogs.PogsMain
{
    public partial class MainForm : Form
    {
        #region Declarations

        private ClientRecord _presortClient;
        private LogViewer _logViewer;
        private SortableBindingList<ClientRecord> _clients;

        private ClientRecord _activeClient;
        private ClientEntry _activeEntry;

        private Dictionary<ClientEntry, EntryNotes> _entryNotes = new Dictionary<ClientEntry, EntryNotes>();
        private Dictionary<ClientEntry, ClientEntryView> _currentClientFields = new Dictionary<ClientEntry, ClientEntryView>();

        private BindingList<EntryFieldRow> _activeFields = new BindingList<EntryFieldRow>();

        private bool _editMode;

        private DataGridViewCellStyle _lightStyle;
        private DataGridViewCellStyle _darkStyle;

        private DataGridViewCellStyle _readOnlyStyle;
        private DataGridViewCellStyle _editStyle;

        #endregion Declarations

        #region Properties

        internal PogsDatabase CurrentDatabase { get; set; }

        #endregion Properties

        public MainForm()
        {
            InitializeComponent();

            _lightStyle = this.fieldDataGridViewTextBoxColumn.DefaultCellStyle;
            _darkStyle = _lightStyle.Clone();
            _darkStyle.BackColor = SystemColors.Info;
            _darkStyle.SelectionBackColor = SystemColors.Info;

            _readOnlyStyle = new DataGridViewCellStyle(fieldsDataGridView.DefaultCellStyle);
            _editStyle = _readOnlyStyle.Clone();
            _readOnlyStyle.BackColor = SystemColors.ControlLightLight;
            _editStyle.BackColor = SystemColors.Window;

            _lightStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            _darkStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            _editStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            _readOnlyStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            clientListDataGridView.CellMouseDown += new DataGridViewCellMouseEventHandler(RecordCurrentClientOnHeaderMouseDown);
        }

        private void RecordCurrentClientOnHeaderMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
                return;

            _presortClient = clientListBindingSource.Current as ClientRecord;
        }

        #region UI Eventhandlers

        private void editTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var te = new TemplateManager(this.CurrentDatabase))
            {
                te.ShowDialog();
                this.BringToFront();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Lock_Click(object sender, EventArgs e)
        {
            if (this.CurrentDatabase == null)
            {
                MessageBox.Show("No database is currently open, so Pogs cannot be locked.");
                return;
            }

            LockPogs();
        }

        private void addClientButton_Click(object sender, EventArgs e)
        {
            using (AddEditClientDialog acd = new AddEditClientDialog())
            {
                if (acd.ShowDialog() == DialogResult.OK)
                {
                    this.CurrentDatabase.AddNewClient(acd.ClientRecord);

                    //find the row in the datagridview and select it immediately
                    var row = clientListDataGridView.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r => r.DataBoundItem == acd.ClientRecord);

                    if (row != null)
                    {
                        row.Selected = true;
                        clientListDataGridView.CurrentCell = row.Cells[0];
                    }
                }
            }
        }

        private void renameClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_activeClient == null)
                return;

            using (AddEditClientDialog acd = new AddEditClientDialog(_activeClient))
            {
                if (acd.ShowDialog() == DialogResult.OK)
                {
                    _activeClient.Name = acd.ClientRecord.Name;
                    _activeClient.ClientId = acd.ClientRecord.ClientId;

                    this.CurrentDatabase.CommitClient(_activeClient);
                }
            }
        }

        private void addEntryButton_Click(object sender, EventArgs e)
        {
            this.CurrentDatabase.RefreshTemplates();

            using (AddEntryDialog aed = new AddEntryDialog(this.CurrentDatabase.Templates))
            {
                if (aed.ShowDialog() == DialogResult.OK)
                {
                    _activeClient.AddEntry(aed.ClientEntry);
                    this.CurrentDatabase.CommitClient(_activeClient);

                    //find the listview item and select it immediately
                    var item = entryListView.Items
                        .Cast<ListViewItem>()
                        .FirstOrDefault(lvi => lvi.Tag == aed.ClientEntry);

                    if (!_editMode)
                        BeginEditMode();

                    if (item != null)
                    {
                        entryListView.SelectedItems.Clear();
                        item.Selected = true;
                        item.EnsureVisible();

                        if (fieldsDataGridView.Rows.Count > 0)
                        {
                            fieldsDataGridView.CurrentCell = fieldsDataGridView[1, 0];
                            fieldsDataGridView.BeginEdit(true);
                        }
                    }
                }
            }
        }

        private void deleteEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_activeClient == null || entryListView.SelectedItems.Count < 1)
                return;
            var entry = entryListView.SelectedItems[0].Tag as ClientEntry;
            if (entry == null)
                return;

            using (var pcd = new ConfirmDeleteDialog(entry))
            {
                if (pcd.ShowDialog() == DialogResult.OK)
                {
                    _activeClient.RemoveEntry(entry);
                    this.CurrentDatabase.CommitClient(_activeClient);
                }
            }
        }

        private void renameEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_activeClient == null || entryListView.SelectedItems.Count < 1)
                return;
            var entry = entryListView.SelectedItems[0].Tag as ClientEntry;
            if (entry == null)
                return;

            using (var aed = new AddEntryDialog(entry))
            {
                if (aed.ShowDialog() == DialogResult.OK)
                {
                    entry.Name = aed.ClientEntry.Name;  //just copy over the name
                    this.CurrentDatabase.CommitEntryChanges(new[] { entry });
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (!_editMode)
                BeginEditMode();
            else
                EndEditMode();
        }

        private void purgeClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_activeClient == null)
                return;

            if (!this.CurrentDatabase.CurrentUser.IsAdmin)
            {
                MessageBox.Show("You cannot purge this client because you are not a Pogs admin.\n\n" +
                                "See your Pogs database administrator for more information.");
                return;
            }

            using (var pcd = new PurgeClientDialog(_activeClient))
            {
                if (pcd.ShowDialog() == DialogResult.OK)
                {
                    this.CurrentDatabase.PurgeClient(_activeClient);
                }
            }
        }

        private void changeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var cdb = new ChooseDatabase())
            {
                if (this.CurrentDatabase != null)
                {
                    cdb.ServerName = this.CurrentDatabase.ServerName;
                    cdb.DatabaseName = this.CurrentDatabase.DatabaseName;
                }

                if (cdb.ShowDialog() == DialogResult.OK)
                {
                    //save the setting
                    UserSettings.SetStringSetting(UserSettings.LastDatabaseKey, cdb.DatabaseName);
                    UserSettings.SetStringSetting(UserSettings.LastServerKey, cdb.ServerName);

                    ConnectToDatabaseFromSettings();
                }
            }
        }

        private void clientContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            var activeClient = activeClientBindingSource.Current as ClientRecord;

            editToolStripMenuItem.Checked = _editMode;
            editToolStripMenuItem.Enabled = (activeClient != null && activeClient.Security.CheckIsAllowedEdit(this.CurrentDatabase.CurrentUser));
            renameToolStripMenuItem.Enabled = (activeClient != null && activeClient.Security.CheckIsAllowedEdit(this.CurrentDatabase.CurrentUser));
            securityToolStripMenuItem.Enabled = (activeClient != null && this.CurrentDatabase.CurrentUser.IsAdminRecursive);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_editMode)
                EndEditMode();

            if (this.CurrentDatabase != null)
            {
                this.CurrentDatabase.Dispose();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConnectToDatabaseFromSettings();

            entryFieldRowBindingSource.DataSource = _activeFields;

            Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }

        private void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (this.CurrentDatabase == null)
                return;

            if (e.Reason == Microsoft.Win32.SessionSwitchReason.SessionLock)
            {
                LockPogs();
            }
        }

        private void logViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_logViewer == null)
                _logViewer = new LogViewer();

            //_logViewer.Show();

            try
            {
                System.Diagnostics.Process.Start("LogViewer.xlsx");
            }
            catch { }
        }

        private void clientRecordBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            //display the current client
            var currentClient = clientListBindingSource.Current as ClientRecord;
            if (currentClient != null)
                this.CurrentDatabase.Refresh(currentClient);

            DisplayClient(currentClient);
        }

        private void entryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySelectedEntry();
        }

        private void DataGridView_SelectOnRightClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //select the clicked row
            if (e.Button == MouseButtons.Right)
            {
                DataGridView grid = (DataGridView)sender;
                grid.ClearSelection();
                grid[e.ColumnIndex, e.RowIndex].Selected = true;
                grid.CurrentCell = grid[e.ColumnIndex, e.RowIndex];
            }
        }

        private void fieldsDataGridView_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                e.ContextMenuStrip = fieldMenuStrip;

                var cell = fieldsDataGridView[e.ColumnIndex, e.RowIndex];

                var fieldRow = fieldsDataGridView.Rows[e.RowIndex].DataBoundItem as EntryFieldRow;

                openSiteInBrowserToolStripMenuItem.Visible = cell.Value != null && (fieldRow != null && fieldRow.SourceField.EntryType == EntryFieldType.Website);
                if (cell.Value != null)
                {
                    string cellValue = cell.Value.ToString();
                    if (cellValue.Length > 18)
                        cellValue = cellValue.Substring(0, 18);

                    openSiteInBrowserToolStripMenuItem.Text = String.Format("Visit {0}...", cellValue);
                }
            }
        }

        private void fieldsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                EntryFieldRow row = fieldsDataGridView.Rows[e.RowIndex].DataBoundItem as EntryFieldRow;
                e.CellStyle = row.IsCustom ? _darkStyle : _lightStyle;
            }
            else if (e.ColumnIndex == 1)
            {
                e.CellStyle = _editMode ? _editStyle : _readOnlyStyle;
            }
        }

        private void clientListDataGridView_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                e.ContextMenuStrip = clientContextMenuStrip;
            }
        }

        private void clientListDataGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // If app key pressed
            if (e.KeyData == Keys.Apps)
            {
                // Find first selected and displayed cell
                DataGridViewCell cell = null;
                foreach (DataGridViewCell eCell in clientListDataGridView.SelectedCells)
                {
                    if (eCell.Displayed)
                    {
                        cell = eCell;
                        break;
                    }
                }

                // If there is a selected and displayed cell
                if (cell != null)
                {
                    ContextMenuStrip strip = cell.ContextMenuStrip;

                    // If that cell has a context menu (provided by our 'needed' handler above)
                    if (strip != null)
                    {
                        // Get the display rectangle of the cell
                        Rectangle cellRect = clientListDataGridView.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true);
                        // Bottom-left corner
                        Point point = new Point(cellRect.Left, cellRect.Bottom);

                        // Convert to screen coordinates
                        point = clientListDataGridView.PointToScreen(point);

                        // Display the context menu
                        strip.Show(point);
                    }
                }
            }
        }

        private void clientListDataGridView_Sorted(object sender, EventArgs e)
        {
            //must re-select the current client
            DataGridViewRow row = clientListDataGridView.Rows
                .Cast<DataGridViewRow>()
                .FirstOrDefault(r => r.DataBoundItem == _presortClient);

            if (row != null)
            {
                clientListDataGridView.ClearSelection();
                row.Selected = true;

                //the cell property has to change or else nothing happens
                int altColumn = clientListDataGridView.CurrentCell.ColumnIndex == 0 ? 1 : 0;
                clientListDataGridView.CurrentCell = row.Cells[altColumn];
            }
        }

        private void addCustomFieldLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_activeEntry == null)
                return;

            using (AddEditFieldDialog aefd = new AddEditFieldDialog())
            {
                if (aefd.ShowDialog() == DialogResult.OK)
                {
                    _activeEntry.AddCustomField(aefd.EntryField);
                }
            }
        }

        private void removeCustomFieldLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_activeEntry == null)
                return;

            //remove the custom field from the entry
            var currentRow = entryFieldRowBindingSource.Current as EntryFieldRow;
            if (currentRow != null)
            {
                if (currentRow.SourceEntry.CustomFields.Contains(currentRow.SourceField))
                {
                    using (DeleteCustomFieldDialog dcfd = new DeleteCustomFieldDialog(currentRow.SourceEntry, currentRow.SourceField))
                    {
                        if (dcfd.ShowDialog() == DialogResult.OK)
                        {
                            currentRow.SourceEntry.RemoveCustomField(currentRow.SourceField);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(String.Format("The '{0}' field is not a custom field.  It is part of the '{1}' template." +
                        "\n\nTo make changes to the template, which will affect the entire database, choose Tools, Edit Templates.",
                        currentRow.FieldName, currentRow.SourceEntry.Template.Name), "Remove Field", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void copyValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewCell activeCell = fieldsDataGridView.CurrentCell;
            if (activeCell != null && activeCell.Value != null)
            {
                Clipboard.SetText(activeCell.Value.ToString());
            }
        }

        private void openSiteInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewCell activeCell = fieldsDataGridView.CurrentCell;
            if (activeCell != null && activeCell.Value != null)
            {
                try
                {
                    System.Diagnostics.Process.Start(activeCell.Value.ToString());
                }
                catch
                { }
            }
        }

        private void aboutPogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ab = new AboutPogs())
            {
                ab.ShowDialog();
            }
        }

        private void editUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CurrentDatabase.RefreshSecurityPrincipals();
            this.CurrentDatabase.Refresh(this.CurrentDatabase.DefaultSecurity);

            using (var vm = new DatabaseSecurityDialogViewModel(this.CurrentDatabase))
            using (var dad = new DatabaseSecurityDialog { DataContext = vm })
            {
                dad.ShowDialog();
            }
        }

        private void securityToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditCurrentClientSecurity();
        }

        private void securityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditCurrentClientSecurity();
        }

        private void toolsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            editUsersToolStripMenuItem.Enabled = this.CurrentDatabase != null && this.CurrentDatabase.CurrentUser.IsAdminRecursive;
        }

        private void clientToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            securityToolStripMenuItem.Enabled = this.CurrentDatabase != null && _activeClient != null && this.CurrentDatabase.CurrentUser.IsAdminRecursive;
        }

        private void changePINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var pinDialog = new ResetPinDialog(this.CurrentDatabase, this.CurrentDatabase.CurrentUser))
            {
                pinDialog.ShowDialog();
            }
        }

        #endregion UI Eventhandlers

        #region Data Model Event Handlers

        private void ActiveEntry_CustomFieldRemoved(object sender, EntryFieldEventArgs e)
        {
            //remove the entryfieldrow from field
            var cache = GetCurrentCachedEntryFieldRows();
            var row = cache[e.EntryField];
            _activeFields.Remove(row);
            cache.Remove(e.EntryField);
            cache.ForceCommit = true;
        }

        private void ActiveEntry_CustomFieldAdded(object sender, EntryFieldEventArgs e)
        {
            //create the entryfieldrow and add to view
            var cache = GetCurrentCachedEntryFieldRows();
            var row = new EntryFieldRow(_activeEntry, e.EntryField);
            cache.Add(e.EntryField, row);
            cache.ForceCommit = true;
            _activeFields.Add(row);
        }

        private void CurrentDatabase_ClientsRemoved(object sender, ClientRecordEventArgs e)
        {
            if (sender != this.CurrentDatabase)
                return;

            foreach (var client in e.ClientRecords)
            {
                _clients.Remove(client);
            }
        }

        private void CurrentDatabase_ClientsAdded(object sender, ClientRecordEventArgs e)
        {
            if (sender != this.CurrentDatabase)
                return;

            foreach (var client in e.ClientRecords)
            {
                _clients.Add(client);
            }
        }

        private void CurrentDatabase_ConnectionUnexpectedlyClosed(object sender, EventArgs e)
        {
            if (sender != this.CurrentDatabase)
                return;

            _clients.Clear();
        }

        private void ClientEntry_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = entryListView.Items.Cast<ListViewItem>().FirstOrDefault(lvi => lvi.Tag == sender);
            if (item != null)
            {
                ClientEntry entry = (ClientEntry)item.Tag;
                switch (e.PropertyName)
                {
                    case "Name":
                        {
                            item.Text = entry.Name;
                        }
                        break;

                    case "IconIndex":
                        {
                            item.ImageIndex = entry.IconIndex;
                        }
                        break;
                }
            }
        }

        private void ClientRecord_EntryAdded(object sender, ClientEntryEventArgs e)
        {
            foreach (var entry in e.ClientEntries)
                AddEntryToListView(entry);
        }

        private void ClientRecord_EntryRemoved(object sender, ClientEntryEventArgs e)
        {
            foreach (var entry in e.ClientEntries)
                RemoveEntryFromListView(entry);
        }

        #endregion Data Model Event Handlers

        #region Private Methods

        private void LockPogs()
        {
            //stop monitoring the system lock state while the lock screen is shown
            Microsoft.Win32.SystemEvents.SessionSwitch -= SystemEvents_SessionSwitch;

            this.Hide();

            using (var ls = new LockScreen(this.CurrentDatabase))
            {
                if (ls.ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                }
                else
                {
                    this.Show();
                }
            }

            Microsoft.Win32.SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
        }

        private void DisplayClient(ClientRecord currentClient)
        {
            if (_editMode)
                EndEditMode();

            //stop listening to the old client
            if (_activeClient != null)
            {
                _activeClient.EntriesAdded -= ClientRecord_EntryAdded;
                _activeClient.EntriesRemoved -= ClientRecord_EntryRemoved;

                foreach (var e in _activeClient.Entries)
                {
                    e.PropertyChanged -= ClientEntry_PropertyChanged;
                }
            }

            entryListView.Items.Clear();
            _currentClientFields.Clear();
            _entryNotes.Clear();

            entryListView.Enabled = (currentClient != null);
            clientNameLabel.Enabled = (currentClient != null);

            _activeClient = currentClient;

            if (currentClient != null)
            {
                activeClientBindingSource.DataSource = currentClient;

                _activeClient.EntriesAdded += new EventHandler<ClientEntryEventArgs>(ClientRecord_EntryAdded);
                _activeClient.EntriesRemoved += new EventHandler<ClientEntryEventArgs>(ClientRecord_EntryRemoved);

                //since the listview doesn't support databinding, we need to simulate it by populating it and listening
                //for changes for both the entry list and each entry
                foreach (var entry in currentClient.Entries)
                {
                    AddEntryToListView(entry);
                }

                entryListView.SelectedItems.Clear();
                if (entryListView.Items.Count > 0)
                    entryListView.Items[0].Selected = true;
                else
                    DisplaySelectedEntry();

                bool editAllowed = currentClient.Security.CheckIsAllowedEdit(this.CurrentDatabase.CurrentUser);

                renameClientToolStripMenuItem.Enabled = editAllowed;
                editButton.Enabled = editAllowed;
                purgeClientToolStripMenuItem.Enabled = editAllowed;
                editModeToolStripMenuItem.Enabled = editAllowed;
                addEntryToolStripMenuItem.Enabled = editAllowed;
                addEntryButton.Enabled = editAllowed;
                deleteEntryToolStripMenuItem.Enabled = editAllowed;
                renameEntryToolStripMenuItem.Enabled = editAllowed;
                securityToolStripMenuItem.Enabled = this.CurrentDatabase.CurrentUser.IsAdmin;
                securityToolStripMenuItem1.Enabled = this.CurrentDatabase.CurrentUser.IsAdmin;
            }
            else
            {
                activeClientBindingSource.DataSource = typeof(ClientRecord);

                renameClientToolStripMenuItem.Enabled = false;
                editButton.Enabled = false;
                purgeClientToolStripMenuItem.Enabled = false;
                editModeToolStripMenuItem.Enabled = false;
                addEntryButton.Enabled = false;
                addEntryToolStripMenuItem.Enabled = false;
                deleteEntryToolStripMenuItem.Enabled = false;
                renameEntryToolStripMenuItem.Enabled = false;
                securityToolStripMenuItem.Enabled = false;
                securityToolStripMenuItem1.Enabled = false;
            }
        }

        private void AddEntryToListView(ClientEntry entry)
        {
            var item = entryListView.Items.Add(entry.Name, entry.IconIndex);
            item.SubItems.Add(entry.Template != null ? entry.Template.Name : String.Empty);
            item.Tag = entry;
            entry.PropertyChanged += new PropertyChangedEventHandler(ClientEntry_PropertyChanged);
        }

        private void RemoveEntryFromListView(ClientEntry entry)
        {
            //find the item and remove it
            ListViewItem item = entryListView.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Tag == entry);
            if (item != null)
                item.Remove();
        }

        private void BeginEditMode()
        {
            if (_editMode)
                throw new InvalidOperationException("Cannot begin edit mode because the current mode is already edit mode.");

            editModeToolStripMenuItem.Checked = true;
            editButton.Text = "End Editing";
            editButton.Checked = true;
            fieldsDataGridView.ReadOnly = false;
            fieldsDataGridView.Columns[0].ReadOnly = true;
            notesTextBox.ReadOnly = false;

            customFieldLabel.Visible = true;
            addCustomFieldLink.Visible = true;
            removeCustomFieldLink.Visible = true;

            _editMode = true;
        }

        private void EndEditMode()
        {
            if (!_editMode)
                throw new InvalidOperationException("Cannot end edit mode because Pogs is not currently in editing mode.");

            fieldsDataGridView.EndEdit();
            entryFieldRowBindingSource.EndEdit();
            entryNotesBindingSource.EndEdit();

            //go through the view model and find any views that are new
            var entriesToCommit = new List<ClientEntry>();
            foreach (var entry in _activeClient.Entries.Where(e => _currentClientFields.ContainsKey(e)))
            {
                var rowCache = _currentClientFields[entry];

                //check the rows first -
                //a row may be flagged as a new custom field, so we always re-save the entry entry
                bool changesMade = rowCache.ForceCommit;
                foreach (var row in rowCache.Values)
                {
                    changesMade |= row.Commit();    //copies data entered in the grid to the data model
                }

                //next we check if the notes have changed, and copy back if necessary
                if (_entryNotes.ContainsKey(entry) && _entryNotes[entry].Notes != entry.Notes)
                {
                    entry.Notes = _entryNotes[entry].Notes;
                    changesMade = true;
                }

                //add to the list if it needs to be committed
                if (changesMade)
                    entriesToCommit.Add(entry);
            }

            //finally, we save any changed entries to the db
            if (entriesToCommit.Any())
                this.CurrentDatabase.CommitEntryChanges(entriesToCommit);

            editButton.Checked = false;
            editButton.Text = "Edit";
            editModeToolStripMenuItem.Checked = false;
            fieldsDataGridView.ReadOnly = true;
            notesTextBox.ReadOnly = true;

            customFieldLabel.Visible = false;
            addCustomFieldLink.Visible = false;
            removeCustomFieldLink.Visible = false;

            _editMode = false;
        }

        private void DisplaySelectedEntry()
        {
            if (_activeEntry != null)
            {
                _activeEntry.CustomFieldAdded -= ActiveEntry_CustomFieldAdded;
                _activeEntry.CustomFieldRemoved -= ActiveEntry_CustomFieldRemoved;
            }

            _activeFields.Clear();

            if (entryListView.SelectedItems.Count > 0)
            {
                _activeEntry = entryListView.SelectedItems[0].Tag as ClientEntry;

                _activeEntry.CustomFieldAdded += new EventHandler<EntryFieldEventArgs>(ActiveEntry_CustomFieldAdded);
                _activeEntry.CustomFieldRemoved += new EventHandler<EntryFieldEventArgs>(ActiveEntry_CustomFieldRemoved);

                entryGroupBox.Text = _activeEntry.Template == null ? _activeEntry.Name : String.Format("{0} ({1})", _activeEntry.Name, _activeEntry.Template.Name);

                EntryNotes notes;
                if (!_entryNotes.TryGetValue(_activeEntry, out notes))
                {
                    notes = new EntryNotes() { Notes = _activeEntry.Notes };
                    _entryNotes.Add(_activeEntry, notes);
                }

                entryNotesBindingSource.DataSource = notes;
                notesTextBox.Enabled = true;

                addCustomFieldLink.Enabled = _activeEntry.Template == null || _activeEntry.Template.AllowCustomFields;
                removeCustomFieldLink.Enabled = _activeEntry.Template == null || _activeEntry.Template.AllowCustomFields;

                //display fields by creating temporary Row objects, which last as long as the client stays selected
                //this allows the user to jump from entry to entry within a client without losing changes
                Dictionary<EntryField, EntryFieldRow> rowCache = GetCurrentCachedEntryFieldRows();

                foreach (var f in _activeEntry.AllFields)
                {
                    EntryFieldRow row;
                    if (!rowCache.TryGetValue(f, out row))
                    {
                        row = new EntryFieldRow(_activeEntry, f);
                        rowCache.Add(f, row);
                    }

                    _activeFields.Add(row);
                }

                deleteEntryToolStripMenuItem.Enabled = true;
                renameEntryToolStripMenuItem.Enabled = true;

                //record the log entry
                this.CurrentDatabase.AddLogEntry(_activeEntry, UserLogEntryType.Access);
            }
            else
            {
                _activeEntry = null;
                _activeFields.Clear();
                entryGroupBox.Text = "(no entry selected)";
                entryNotesBindingSource.DataSource = typeof(EntryNotes);
                notesTextBox.Enabled = false;
                deleteEntryToolStripMenuItem.Enabled = false;
                renameEntryToolStripMenuItem.Enabled = false;
                addCustomFieldLink.Enabled = false;
                removeCustomFieldLink.Enabled = false;
            }
        }

        private void EditCurrentClientSecurity()
        {
            try
            {
                this.CurrentDatabase.RefreshSecurityPrincipals();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Security cannot be edited for this user because Pogs could not refresh the list of users and groups.\n\n{0}", ex.Message), String.Empty,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var currentClient = clientListBindingSource.Current as ClientRecord;
            try
            {
                this.CurrentDatabase.Refresh(currentClient.Security);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Security cannot be edited for this user because Pogs could not refresh the list of users and groups.\n\n{0}", ex.Message), String.Empty,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (currentClient != null)
            {
                using (var viewModel = new ClientSecurityDialogViewModel(currentClient, this.CurrentDatabase))
                using (var securityDialog = new ClientSecurityDialog { DataContext = viewModel })
                {
                    securityDialog.ShowDialog();
                }
            }
        }

        private ClientEntryView GetCurrentCachedEntryFieldRows()
        {
            ClientEntryView rowCache;
            if (!_currentClientFields.TryGetValue(_activeEntry, out rowCache))
            {
                rowCache = new ClientEntryView(_activeEntry);
                _currentClientFields.Add(_activeEntry, rowCache);
            }
            return rowCache;
        }

        private void ConnectToDatabaseFromSettings()
        {
            if (UserSettings.HasSetting(UserSettings.LastServerKey) && UserSettings.HasSetting(UserSettings.LastDatabaseKey))
            {
                try
                {
                    SetCurrentDatabase(new PogsDatabase(Environment.UserName)
                    {
                        DatabaseName = UserSettings.GetStringSetting(UserSettings.LastDatabaseKey),
                        ServerName = UserSettings.GetStringSetting(UserSettings.LastServerKey)
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Pogs encountered a problem when connecting to the database '{0}' on '{1}'.\n\nDetails:\n{2}",
                        UserSettings.GetStringSetting(UserSettings.LastDatabaseKey), UserSettings.GetStringSetting(UserSettings.LastServerKey), ex.Message));

                    SetCurrentDatabase(null);
                }
            }
        }

        private void SetCurrentDatabase(PogsDatabase pogsDatabase)
        {
            if (pogsDatabase != null)
            {
                using (var ls = new LockScreen(pogsDatabase))
                {
                    if (ls.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }
            }

            if (this.CurrentDatabase != null)
            {
                this.CurrentDatabase.ConnectionUnexpectedlyClosed -= new EventHandler(CurrentDatabase_ConnectionUnexpectedlyClosed);
                this.CurrentDatabase.ClientsAdded -= new EventHandler<ClientRecordEventArgs>(CurrentDatabase_ClientsAdded);
                this.CurrentDatabase.ClientsRemoved -= new EventHandler<ClientRecordEventArgs>(CurrentDatabase_ClientsRemoved);
                this.CurrentDatabase.Dispose();
            }

            this.CurrentDatabase = pogsDatabase;

            if (this.CurrentDatabase == null)
            {
                this.Text = "Pogs";
                _clients = null;
                clientListBindingSource.DataSource = typeof(ClientRecord);
                addClientButton.Enabled = false;
                newClientToolStripMenuItem.Enabled = false;
                changePINToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.Text = String.Format("Pogs - [{0} on {1}]", pogsDatabase.DatabaseName, pogsDatabase.ServerName);

                this.CurrentDatabase.Refresh(this.CurrentDatabase.DefaultSecurity);
                this.CurrentDatabase.RefreshClientList();

                //set up the initial client list
                _clients = new SortableBindingList<ClientRecord>();
                foreach (var client in this.CurrentDatabase.Clients)
                    _clients.Add(client);

                clientListBindingSource.DataSource = _clients;

                //listen for future changes
                this.CurrentDatabase.ConnectionUnexpectedlyClosed += new EventHandler(CurrentDatabase_ConnectionUnexpectedlyClosed);
                this.CurrentDatabase.ClientsAdded += new EventHandler<ClientRecordEventArgs>(CurrentDatabase_ClientsAdded);
                this.CurrentDatabase.ClientsRemoved += new EventHandler<ClientRecordEventArgs>(CurrentDatabase_ClientsRemoved);

                //set up the UI
                addClientButton.Enabled = this.CurrentDatabase.CurrentUser.IsAdmin;
                newClientToolStripMenuItem.Enabled = true;
                changePINToolStripMenuItem.Enabled = true;
            }
        }

        #endregion Private Methods
    }
}