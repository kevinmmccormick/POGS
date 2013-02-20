using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pogs.DataModel;

namespace Pogs.PogsMain
{
    public partial class ConfirmDeleteDialog : Form
    {
        public ConfirmDeleteDialog(ClientEntry entry)
        {
            InitializeComponent();

            mainInstructionLabel.Text = String.Format(mainInstructionLabel.Text, entry.Name, entry.Client.Name);
        }

        public ConfirmDeleteDialog(string mainInstruction, string details, string commitText, string caption)
        {
            InitializeComponent();

            mainInstructionLabel.Text = mainInstruction;
            detailsLabel.Text = details;
            purgeButton.Text = commitText;
            this.Text = caption;
        }
    }
}