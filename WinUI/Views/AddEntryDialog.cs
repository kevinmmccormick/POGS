using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Pogs.DataModel;

namespace Pogs.PogsMain
{
    public partial class AddEntryDialog : Form
    {
        internal ClientEntry ClientEntry { get; private set; }

        public AddEntryDialog(IEnumerable<EntryTemplate> templates)
        {
            InitializeComponent();

            this.ClientEntry = new ClientEntry();

            clientEntryBindingSource.DataSource = this.ClientEntry;
            entryTemplateBindingSource.DataSource = templates.OrderBy(t => t.Name).ToList() ?? Enumerable.Empty<EntryTemplate>();
        }

        public AddEntryDialog(ClientEntry entry)
        {
            InitializeComponent();

            this.Text = "Rename Entry";
            this.ClientEntry = new ClientEntry() { Template = entry.Template, Name = entry.Name };

            clientEntryBindingSource.DataSource = this.ClientEntry;

            if (entry.Template != null)
                entryTemplateBindingSource.DataSource = new[] { entry.Template };

            templateComboBox.Enabled = false;
        }

        private void entryTemplateBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            this.ClientEntry.Template = entryTemplateBindingSource.Current as EntryTemplate;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.ClientEntry.Name) || this.ClientEntry.Name.Trim().Length == 0)
                MessageBox.Show("Entry must have a name.");
            else
                this.DialogResult = DialogResult.OK;
        }
    }
}