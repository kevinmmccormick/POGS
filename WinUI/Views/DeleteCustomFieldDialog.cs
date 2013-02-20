using System;
using System.Windows.Forms;
using Pogs.DataModel;

namespace Pogs.PogsMain
{
    public partial class DeleteCustomFieldDialog : Form
    {
        public DeleteCustomFieldDialog(ClientEntry container, EntryField field)
        {
            InitializeComponent();

            mainInstructionLabel.Text = String.Format(mainInstructionLabel.Text, field.Name, container.Name);
        }
    }
}