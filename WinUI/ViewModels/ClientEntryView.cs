using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pogs.DataModel;

namespace Pogs.VisualModel
{
    internal class ClientEntryView : Dictionary<EntryField, EntryFieldRow>
    {
        internal ClientEntry ClientEntry { get; set; }

        internal bool ForceCommit { get; set; }

        internal ClientEntryView(ClientEntry entry)
        {
            this.ClientEntry = entry;
        }
    }
}