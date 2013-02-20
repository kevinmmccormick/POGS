using System;
using System.Drawing;
using System.Windows.Forms;
using Pogs.DataModel;

namespace Pogs.PogsMain
{
    public partial class LockScreen : Form
    {
        #region Declarations

        private PogsDatabase _database;

        #endregion

        #region Constructors

        public LockScreen()
        {
            InitializeComponent();
        }

        internal LockScreen(PogsDatabase database)
            : this()
        {
            _database = database;
            unlockInstructions.Text = String.Format(unlockInstructions.Text, database.DatabaseName, database.ServerName);
        }

        #endregion

        #region UI Eventhandlers

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tb.Text.Length == 1)
            {
                MoveNext(tb);
            }
        }

        private void MoveNext(TextBox tb)
        {
            switch (tb.Name)
            {
                case "digit1TextBox":
                    digit2TextBox.Focus();
                    break;

                case "digit2TextBox":
                    digit3TextBox.Focus();
                    break;

                case "digit3TextBox":
                    digit4TextBox.Focus();
                    break;
            }
        }

        private void MovePrev(TextBox tb)
        {
            switch (tb.Name)
            {
                case "digit2TextBox":
                    digit1TextBox.Focus();
                    break;

                case "digit3TextBox":
                    digit2TextBox.Focus();
                    break;

                case "digit4TextBox":
                    digit3TextBox.Focus();
                    break;
            };
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.BackColor = SystemColors.Highlight;
            tb.ForeColor = SystemColors.HighlightText;
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.BackColor = SystemColors.Window;
            tb.ForeColor = SystemColors.ControlText;
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    tb.Text = String.Empty;
                    break;

                case Keys.Back:
                    tb.Text = String.Empty;
                    MovePrev(tb);
                    break;

                case Keys.Left:
                    MovePrev(tb);
                    break;

                case Keys.Right:
                    MoveNext(tb);
                    break;
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            //replace text logic
            if (tb.Text.Length > 0)
            {
                tb.Text = e.KeyChar.ToString();
            }
        }

        private void unlockButton_Click(object sender, EventArgs e)
        {
            string pin = String.Format("{0}{1}{2}{3}", digit1TextBox.Text, digit2TextBox.Text, digit3TextBox.Text, digit4TextBox.Text);
            if (_database.Authenticate(pin))
            {
                this.Hide();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("The PIN entered is incorrect.");
                unlockButton.Enabled = false;
                waitTimer.Enabled = true;
            }
        }

        private void waitTimer_Tick(object sender, EventArgs e)
        {
            waitTimer.Enabled = false;
            unlockButton.Enabled = true;
        }

        #endregion
    }
}