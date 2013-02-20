using System;
using System.Windows.Forms;
using Pogs.DataModel;

namespace Pogs.PogsMain
{
    public partial class PurgeClientDialog : Form
    {
        public PurgeClientDialog(ClientRecord client)
        {
            InitializeComponent();

            mainInstructionLabel.Text = String.Format(mainInstructionLabel.Text, client.Name);
        }
    }
}