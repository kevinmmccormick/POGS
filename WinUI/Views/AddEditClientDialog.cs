using System;
using System.Windows.Forms;
using Pogs.DataModel;

namespace Pogs.PogsMain
{
    public partial class AddEditClientDialog : Form
    {
        public ClientRecord ClientRecord { get; private set; }

        public AddEditClientDialog()
        {
            InitializeComponent();

            this.ClientRecord = new ClientRecord();
            clientRecordBindingSource.DataSource = this.ClientRecord;
        }

        public AddEditClientDialog(ClientRecord clientToEdit)
        {
            InitializeComponent();

            this.ClientRecord = new ClientRecord() { Name = clientToEdit.Name, ClientId = clientToEdit.ClientId };
            clientRecordBindingSource.DataSource = this.ClientRecord;

            this.Text = "Rename Client";
            commitButton.Text = "Save";
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            clientRecordBindingSource.EndEdit();

            if (this.ClientRecord.Name == null || this.ClientRecord.Name.Trim() == String.Empty)
            {
                MessageBox.Show("Client name cannot be blank.");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}