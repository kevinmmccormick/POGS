using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pogs.PogsMain
{
    public partial class ChooseDatabase : Form
    {
        internal string DatabaseName
        {
            get { return databaseComboBox.Text; }
            set { databaseComboBox.Text = value; }
        }

        internal string ServerName
        {
            get { return serverNameTextBox.Text; }
            set { serverNameTextBox.Text = value; }
        }

        public ChooseDatabase()
        {
            InitializeComponent();
        }

        private void checkServerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PopulateDatabaseNames();
        }

        private void PopulateDatabaseNames()
        {
            databaseComboBox.Items.Clear();

            if (String.IsNullOrEmpty(serverNameTextBox.Text))
                return;

            try
            {
                using (var tempConn = new SqlConnection(GetTempConnectionString()))
                {
                    Cursor.Current = Cursors.WaitCursor;

                    tempConn.Open();

                    var SqlCom = new System.Data.SqlClient.SqlCommand()
                    {
                        Connection = tempConn,
                        CommandText = "SELECT name FROM master.sys.databases WHERE has_dbaccess(name) = 1 and name NOT IN ('master', 'tempdb', 'model', 'msdb') ORDER BY name"
                    };

                    var reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        databaseComboBox.Items.Add(reader.GetString(0));
                    }

                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    String.Format("Pogs was unable to get the list of databases on this server. ({0})", ex.Message),
                    String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetTempConnectionString()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = serverNameTextBox.Text,
                IntegratedSecurity = true
            };

            return builder.ToString();
        }

        private void databaseComboBox_TextChanged(object sender, EventArgs e)
        {
            okButton.Enabled = !String.IsNullOrEmpty(databaseComboBox.Text);
        }

        private void databaseComboBox_DropDown(object sender, EventArgs e)
        {
            PopulateDatabaseNames();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            PopulateDatabaseNames();

            if (!databaseComboBox.Items.Contains(databaseComboBox.Text))
            {
                if (MessageBox.Show(String.Format("The database '{0}' does not exist on server '{1}'.  Continue anyway?",
                    databaseComboBox.Text, serverNameTextBox.Text), String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}