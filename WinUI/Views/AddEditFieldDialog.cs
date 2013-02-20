using System;
using System.Windows.Forms;
using Pogs.DataModel;

namespace Pogs.PogsMain
{
    public partial class AddEditFieldDialog : Form
    {
        private EntryField _entryField;

        internal EntryField EntryField
        {
            get { return _entryField; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                if (_entryField != value)
                {
                    _entryField = value;

                    nameTextBox.Text = value.Name;
                    typeComboBox.SelectedIndex = (int)value.EntryType;
                }
            }
        }

        public AddEditFieldDialog()
        {
            InitializeComponent();

            this.EntryField = new EntryField();
        }

        public AddEditFieldDialog(EntryField entryField)
        {
            if (entryField == null)
                throw new ArgumentNullException("entryField");

            InitializeComponent();

            this.EntryField = entryField;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text.Trim() == String.Empty)
            {
                MessageBox.Show("The name cannot be blank.");
                return;
            }

            this.EntryField.Name = nameTextBox.Text.Trim();
            this.EntryField.EntryType = (EntryFieldType)typeComboBox.SelectedIndex;

            this.DialogResult = DialogResult.OK;
        }
    }
}