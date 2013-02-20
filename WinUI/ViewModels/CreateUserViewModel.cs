using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using Pogs.DataModel;
using Pogs.DataModel.Security;
using Pogs.PogsMain;
using Pogs.ViewModels;

namespace Pogs.WinUI.ViewModels
{
    public class CreateUserViewModel : ViewModel<PogsDatabase>, IDataErrorInfo
    {
        private string _username;
        private string _pin;

        public CreateUserCommand Create { get; private set; }

        public string Username
        {
            get { return _username; }
            set
            {
                if (_username == value)
                    return;

                _username = value;
                NotifyPropertyChanged("Username");
                this.Create.NotifyCanExecuteChanged();
            }
        }

        public string PIN
        {
            get { return _pin; }
            set
            {
                if (_pin == value)
                    return;

                _pin = value;
                NotifyPropertyChanged("PIN");
                this.Create.NotifyCanExecuteChanged();
            }
        }

        internal UserPrincipal CreatedUser { get; private set; }

        public CreateUserViewModel(PogsDatabase database)
            : base(database)
        {
            this.Create = new CreateUserCommand(this);
        }

        #region IDataErrorInfo Members

        public string Error
        {
            get
            {
                IDataErrorInfo error = (IDataErrorInfo)this;
                var messages = new List<string>(2);
                if (!String.IsNullOrEmpty(error["Username"]))
                    messages.Add(error["Username"]);
                if (!String.IsNullOrEmpty(error["PIN"]))
                    messages.Add(error["PIN"]);

                return String.Join("\n", messages.ToArray());
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Username":
                        if (!String.IsNullOrEmpty(this.Username) && this.Model.SecurityPrincipals.OfType<UserPrincipal>().Any(up => up.Name == this.Username.Trim()))
                            return String.Format("The user '{0}' already exists in this database.", this.Username.Trim());
                        break;

                    case "PIN":
                        if (!String.IsNullOrEmpty(this.PIN) && !ResetPinDialog.ResetPinViewModel.VALID_PIN.IsMatch(this.PIN))
                            return "The PIN entered must be a 4-digit number. (No other characters are allowed.)";
                        break;
                }

                return String.Empty;
            }
        }

        #endregion IDataErrorInfo Members

        public class CreateUserCommand : ChildCommand<CreateUserViewModel>, IDialogResultCommand
        {
            internal CreateUserCommand(CreateUserViewModel parent)
                : base(parent)
            {
                this.DependentProperties.Add("Username");
                this.DependentProperties.Add("PIN");
            }

            protected override bool CanExecuteCore()
            {
                return !String.IsNullOrEmpty(this.Parent.Username) && !String.IsNullOrEmpty(this.Parent.PIN) && String.IsNullOrEmpty(this.Parent.Error);
            }

            protected override void ExecuteCore()
            {
                try
                {
                    //actually add the user to the database
                    this.Parent.CreatedUser = this.Parent.Model.CreateUser(this.Parent.Username, this.Parent.PIN);
                    this.LastResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Pogs was unable to create the user in the database.\n\n" + ex.Message);
                    this.LastResult = DialogResult.None;
                }
            }

            #region IDialogResultCommand Members

            public DialogResult LastResult { get; private set; }

            #endregion IDialogResultCommand Members
        }
    }
}