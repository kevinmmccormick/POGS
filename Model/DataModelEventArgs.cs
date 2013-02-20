using System;
using System.Collections.Generic;

namespace Pogs.DataModel
{
    public class ClientRecordEventArgs : EventArgs
    {
        public IEnumerable<ClientRecord> ClientRecords { get; set; }

        public ClientRecordEventArgs(IEnumerable<ClientRecord> client)
        {
            this.ClientRecords = client;
        }

        public ClientRecordEventArgs(ClientRecord client)
        {
            this.ClientRecords = new[] { client };
        }
    }

    public class ClientEntryEventArgs : EventArgs
    {
        public IEnumerable<ClientEntry> ClientEntries { get; set; }

        internal ClientEntryEventArgs(IEnumerable<ClientEntry> entries)
        {
            this.ClientEntries = entries;
        }

        internal ClientEntryEventArgs(ClientEntry entry)
        {
            this.ClientEntries = new[] { entry };
        }
    }

    public class EntryFieldEventArgs : EventArgs
    {
        public EntryField EntryField { get; set; }

        internal EntryFieldEventArgs(EntryField entryField)
        {
            this.EntryField = entryField;
        }
    }
}