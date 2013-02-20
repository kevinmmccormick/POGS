using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Pogs.DataModel.Security;

namespace Pogs.DataModel
{
    /// <summary>
    /// A single client in a Pogs database.
    /// </summary>
    public class ClientRecord : INotifyPropertyChanged
    {
        #region Declarations

        private List<ClientEntry> _entries = new List<ClientEntry>();

        private string _name;
        private string _clientId;

        #endregion Declarations

        #region Properties

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChange("Name");
                }
            }
        }

        public string ClientId
        {
            get { return _clientId; }
            set
            {
                if (_clientId != value)
                {
                    _clientId = value;
                    NotifyPropertyChange("ClientId");
                }
            }
        }

        public SecurityDescriptorCollection Security { get; private set; }

        public IEnumerable<ClientEntry> Entries
        {
            get { return _entries.AsReadOnly(); }
        }

        #endregion Properties

        #region Constructors

        public ClientRecord()
        {
            this.Security = new SecurityDescriptorCollection { Owner = this };
        }

        #endregion Constructors

        #region Public Methods

        public void AddEntry(ClientEntry entry)
        {
            AddEntries(new[] { entry });
        }

        public void RemoveEntry(ClientEntry entry)
        {
            RemoveEntries(new[] { entry });
        }

        public void AddEntries(IEnumerable<ClientEntry> entries)
        {
            foreach (var e in entries)
            {
                if (_entries.Contains(e))
                    throw new ArgumentException("This client already contains this entry.");
                if (e.Client != null)
                    throw new ArgumentException("This entry is already assigned to a client.");

                _entries.Add(e);
                e.Client = this;
            }

            if (entries.Any())
                OnEntriesAdded(new ClientEntryEventArgs(entries));
        }

        public void RemoveEntries(IEnumerable<ClientEntry> entries)
        {
            foreach (var e in entries.ToList())
            {
                if (!_entries.Contains(e))
                    throw new ArgumentException("This client does not contain this entry.");

                _entries.Remove(e);
            }

            if (entries.Any())
                OnEntriesRemoved(new ClientEntryEventArgs(entries));
        }

        #endregion Public Methods

        #region Custom Events

        public event EventHandler<ClientEntryEventArgs> EntriesAdded;

        protected void OnEntriesAdded(ClientEntryEventArgs e)
        {
            if (EntriesAdded != null)
            {
                EntriesAdded(this, e);
            }
        }

        public event EventHandler<ClientEntryEventArgs> EntriesRemoved;

        protected void OnEntriesRemoved(ClientEntryEventArgs e)
        {
            if (EntriesRemoved != null)
            {
                EntriesRemoved(this, e);
            }
        }

        #endregion Custom Events

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        protected void NotifyPropertyChange(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged Members
    }
}