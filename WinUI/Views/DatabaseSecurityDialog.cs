using System;
using System.Windows.Forms;
using Pogs.ViewModels;
using Pogs.Views;

namespace Pogs.PogsMain
{
    public partial class DatabaseSecurityDialog : MvvmForm
    {
        public DatabaseSecurityDialogViewModel ViewModel
        {
            get { return this.DataContext as DatabaseSecurityDialogViewModel; }
        }

        public DatabaseSecurityDialog()
        {
            InitializeComponent();

            membersGroupBox.DataBindings["Text"].Format += (sender, args) =>
                {
                    string name = (args.Value ?? String.Empty).ToString().Trim();
                    if (name.Length == 0)
                        name = "(unnamed group)";

                    args.Value = String.Format("'{0}' Members", name);
                };
        }

        private void groupsDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (groupsDataGridView.CurrentCell is DataGridViewCheckBoxCell)
                groupsDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void usersDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (usersDataGridView.CurrentCell is DataGridViewCheckBoxCell)
                usersDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void usersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.ViewModel == null)
                return;

            if (usersDataGridView.SelectedRows.Count == 0)
                this.ViewModel.SelectedUser = null;
            else
                this.ViewModel.SelectedUser = usersDataGridView.SelectedRows[0].DataBoundItem as SecurityPrincipalViewModel;
        }

        private void groupsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.ViewModel == null)
                return;

            if (groupsDataGridView.SelectedRows.Count == 0)
                this.ViewModel.SelectedGroup = null;
            else
                this.ViewModel.SelectedGroup = groupsDataGridView.SelectedRows[0].DataBoundItem as GroupPrincipalViewModel;
        }

        private void membersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.ViewModel == null)
                return;

            if (membersDataGridView.SelectedRows.Count == 0)
                this.ViewModel.SelectedGroupMember = null;
            else
                this.ViewModel.SelectedGroupMember = membersDataGridView.SelectedRows[0].DataBoundItem as SecurityPrincipalViewModel;
        }
    }
}