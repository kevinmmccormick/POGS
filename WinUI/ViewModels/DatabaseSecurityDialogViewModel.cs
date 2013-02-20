using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Pogs.DataModel;
using Pogs.DataModel.Security;
using Pogs.PogsMain;
using Pogs.WinUI.ViewModels;
using Pogs.WinUI.Views;

namespace Pogs.ViewModels
{
    public class DatabaseSecurityDialogViewModel : EditableViewModel<PogsDatabase>
    {
        private SecurityPrincipalViewModel _selectedUser;
        private GroupPrincipalViewModel _selectedGroup;
        private SecurityPrincipalViewModel _selectedGroupMember;

        public string Title
        {
            get { return String.Format("Database Security - {0} on {1}", this.Model.DatabaseName, this.Model.ServerName); ; }
        }

        public BindingList<SecurityPrincipalViewModel> Users { get; private set; }

        public BindingList<GroupPrincipalViewModel> Groups { get; private set; }

        public ICommand AddNewUser { get; private set; }

        public ICommand RemoveSelectedUser { get; private set; }

        public ICommand ChangeSelectedUserPin { get; private set; }

        public SecurityPrincipalViewModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                if (_selectedUser == value)
                    return;

                _selectedUser = value;

                ((ChangePinCommand)this.ChangeSelectedUserPin).NotifyCanExecuteChanged();
                ((RemoveUserCommand)this.RemoveSelectedUser).NotifyCanExecuteChanged();
            }
        }

        public ICommand AddNewGroup { get; private set; }

        public ICommand RemoveSelectedGroup { get; private set; }

        public GroupPrincipalViewModel SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (_selectedGroup == value)
                    return;

                _selectedGroup = value;
                ((RemoveGroupCommand)this.RemoveSelectedGroup).NotifyCanExecuteChanged();
                ((AddNewGroupMemberCommand)this.AddNewGroupMember).NotifyCanExecuteChanged();
                ((RemoveGroupMemberCommand)this.RemoveSelectedGroupMember).NotifyCanExecuteChanged();
            }
        }

        public SecurityPrincipalViewModel SelectedGroupMember
        {
            get { return _selectedGroupMember; }
            set
            {
                if (_selectedGroupMember == value)
                    return;

                _selectedGroupMember = value;
                ((RemoveGroupMemberCommand)this.RemoveSelectedGroupMember).NotifyCanExecuteChanged();
            }
        }

        public ICommand AddNewGroupMember { get; private set; }

        public ICommand RemoveSelectedGroupMember { get; private set; }

        public SecurityEditorViewModel DefaultSecurity { get; private set; }

        public IDialogResultCommand Ok { get; private set; }

        public DatabaseSecurityDialogViewModel(PogsDatabase database)
            : base(database)
        {
            this.DefaultSecurity = new SecurityEditorViewModel(database.DefaultSecurity, database.SecurityPrincipals);
            this.Users = new BindingList<SecurityPrincipalViewModel>();
            this.Groups = new BindingList<GroupPrincipalViewModel>();

            InitializeCommands();

            foreach (var user in database.SecurityPrincipals.OfType<UserPrincipal>())
                this.Users.Add(new SecurityPrincipalViewModel(user));

            foreach (var group in database.SecurityPrincipals.OfType<GroupPrincipal>())
                if (group != GroupPrincipal.Everyone)
                    this.Groups.Add(new GroupPrincipalViewModel(group));
        }

        private void InitializeCommands()
        {
            this.AddNewUser = new AddNewUserCommand(this);
            this.RemoveSelectedUser = new RemoveUserCommand(this);
            this.ChangeSelectedUserPin = new ChangePinCommand(this);

            this.AddNewGroup = new AddNewGroupCommand(this);
            this.RemoveSelectedGroup = new RemoveGroupCommand(this);

            this.AddNewGroupMember = new AddNewGroupMemberCommand(this);
            this.RemoveSelectedGroupMember = new RemoveGroupMemberCommand(this);

            this.Ok = new OkCommand(this);
        }

        public override void Commit()
        {
            try
            {
                //commit user changes to data model
                foreach (var group in this.Groups)
                    group.Commit();
                foreach (var user in this.Users)
                    user.Commit();

                this.DefaultSecurity.Commit();

                //commit changes to the database
                //TODO-future: use a unit of work instead of this
                this.Model.CommitSecurityPrincipals(
                     this.Groups.Select(g => g.SecurityPrincipal).Cast<SecurityPrincipal>().Concat(
                     this.Users.Select(g => g.SecurityPrincipal).Cast<SecurityPrincipal>()));
                this.Model.CommitDefaultSecurity();
            }
            catch
            {
                //uncommit the user changes from memory
                foreach (var group in this.Groups)
                    group.UndoCommit();
                foreach (var user in this.Users)
                    user.UndoCommit();

                this.DefaultSecurity.UndoCommit();
                throw;
            }
        }

        #region Commands

        public class AddNewUserCommand : ChildCommand<DatabaseSecurityDialogViewModel>
        {
            public AddNewUserCommand(DatabaseSecurityDialogViewModel parent)
                : base(parent)
            { }

            protected override bool CanExecuteCore()
            {
                return true;
            }

            protected override void ExecuteCore()
            {
                var vm = new CreateUserViewModel(this.Parent.Model);
                using (var dialog = new CreateUserDialog { DataContext = vm })
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        this.Parent.Users.Add(new SecurityPrincipalViewModel(vm.CreatedUser));
                    }
                }
            }
        }

        public class AddNewGroupCommand : ChildCommand<DatabaseSecurityDialogViewModel>
        {
            public AddNewGroupCommand(DatabaseSecurityDialogViewModel parent)
                : base(parent)
            { }

            protected override bool CanExecuteCore()
            {
                return true;
            }

            protected override void ExecuteCore()
            {
                this.Parent.Groups.Add(new GroupPrincipalViewModel(new GroupPrincipal()));
            }
        }

        public class RemoveUserCommand : ChildCommand<DatabaseSecurityDialogViewModel>
        {
            public RemoveUserCommand(DatabaseSecurityDialogViewModel parent)
                : base(parent)
            { }

            protected override bool CanExecuteCore()
            {
                return this.Parent.SelectedUser != null;
            }

            protected override void ExecuteCore()
            {
                using (var cdd = new ConfirmDeleteDialog(String.Format("Are you sure you want to permanently delete the user '{0}'?", this.Parent.SelectedUser.Name),
                    String.Format("'{0}' will no longer be able to access Pogs.", this.Parent.SelectedUser.Name), "Delete User", "Delete User"))
                {
                    if (cdd.ShowDialog() != DialogResult.OK)
                        return;
                }

                var user = this.Parent.SelectedUser;
                user.UndoCommit();    //undo changes to the datamodel in the event the user hits cancel
                this.Parent.SelectedUser = null;
                this.Parent.Users.Remove(user);
            }
        }

        public class RemoveGroupCommand : ChildCommand<DatabaseSecurityDialogViewModel>
        {
            public RemoveGroupCommand(DatabaseSecurityDialogViewModel parent)
                : base(parent)
            { }

            protected override bool CanExecuteCore()
            {
                return this.Parent.SelectedGroup != null;
            }

            protected override void ExecuteCore()
            {
                using (var cdd = new ConfirmDeleteDialog(String.Format("Are you sure you want to permanently delete the group '{0}'?", this.Parent.SelectedGroup.Name),
                        String.Empty, "Delete Group", "Delete Group"))
                {
                    if (cdd.ShowDialog() != DialogResult.OK)
                        return;
                }

                var group = this.Parent.SelectedGroup;
                this.Parent.SelectedGroup = null;
                this.Parent.Groups.Remove(group);
            }
        }

        public class AddNewGroupMemberCommand : ChildCommand<DatabaseSecurityDialogViewModel>
        {
            public AddNewGroupMemberCommand(DatabaseSecurityDialogViewModel parent)
                : base(parent)
            { }

            protected override bool CanExecuteCore()
            {
                return this.Parent.SelectedGroup != null;
            }

            protected override void ExecuteCore()
            {
                var group = (GroupPrincipal)this.Parent.SelectedGroup.SecurityPrincipal;

                using (var viewModel = new SecurityPrincipalSelectorViewModel(this.Parent.Model.SecurityPrincipals
                    .OfType<UserPrincipal>()
                    .Cast<SecurityPrincipal>()
                    .Except(group.Members)))
                using (var dialog = new SecurityPrincipalSelectorDialog { DataContext = viewModel })
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (var selected in viewModel.Selected)
                        {
                            if (!group.ContainsMember(selected.SecurityPrincipal))
                                group.AddMember(selected.SecurityPrincipal);
                        }
                    }
                }
            }
        }

        public class RemoveGroupMemberCommand : ChildCommand<DatabaseSecurityDialogViewModel>
        {
            public RemoveGroupMemberCommand(DatabaseSecurityDialogViewModel parent)
                : base(parent)
            { }

            protected override bool CanExecuteCore()
            {
                return this.Parent.SelectedGroup != null && this.Parent.SelectedGroupMember != null;
            }

            protected override void ExecuteCore()
            {
                this.Parent.SelectedGroup.MemberViewModels.Remove(this.Parent.SelectedGroupMember);
            }
        }

        public class ChangePinCommand : ChildCommand<DatabaseSecurityDialogViewModel>
        {
            public ChangePinCommand(DatabaseSecurityDialogViewModel parent)
                : base(parent)
            { }

            protected override bool CanExecuteCore()
            {
                return this.Parent.SelectedUser != null;
            }

            protected override void ExecuteCore()
            {
                using (var resetDialog = new ResetPinDialog(this.Parent.Model, (UserPrincipal)this.Parent.SelectedUser.SecurityPrincipal))
                {
                    resetDialog.ShowDialog();
                }
            }
        }

        public class OkCommand : ChildCommand<DatabaseSecurityDialogViewModel>, IDialogResultCommand
        {
            public OkCommand(DatabaseSecurityDialogViewModel parent)
                : base(parent)
            { }

            protected override bool CanExecuteCore()
            {
                return true;
            }

            protected override void ExecuteCore()
            {
                if (!VerifyValidSettings())
                    return;

                try
                {
                    this.Parent.Commit();
                    this.LastResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    this.LastResult = DialogResult.None;
                    MessageBox.Show("Pogs encountered a problem when trying to save changes to the security settings.\n\n" + ex.Message);
                }
            }

            private bool VerifyValidSettings()
            {
                //verify that sane settings are entered, such as:

                //a. groups contain deleted users
                if (this.Parent.Groups.Any(g => g.MemberViewModels.Any(m => !this.Parent.Users.Any(u => u.SecurityPrincipal == m.SecurityPrincipal))))
                {
                    MessageBox.Show("Some deleted users still exist in groups.\n\nRemove these users first, and try again.");
                    return false;
                }

                //b. at least 1 admin user
                if (!this.Parent.Users.Any(u => u.IsAdmin) && !this.Parent.Groups.Any(g => g.IsAdmin && g.MemberViewModels.Any()))
                {
                    MessageBox.Show("At least one user or group must have admin permissions.");
                    return false;
                }

                return true;
            }

            public DialogResult LastResult { get; private set; }
        }

        #endregion Commands
    }
}