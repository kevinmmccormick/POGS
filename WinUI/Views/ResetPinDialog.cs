using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Pogs.DataModel;
using Pogs.DataModel.Security;
using Pogs.ViewModels;

namespace Pogs.PogsMain
{
    public partial class ResetPinDialog : Form
    {
        private ResetPinViewModel _viewModel;

        public UserPrincipal UserPrincipal { get; private set; }

        public PogsDatabase Database { get; private set; }

        public ResetPinDialog(PogsDatabase database, UserPrincipal user)
        {
            if (database == null)
                throw new ArgumentNullException("database");
            if (user == null)
                throw new ArgumentNullException("user");

            InitializeComponent();

            label2.Text = String.Format(label2.Text, user.Name);
            _viewModel = new ResetPinViewModel(user, database);
            resetPinViewModelBindingSource.DataSource = _viewModel;

            this.UserPrincipal = user;
            this.Database = database;

            this.errorProvider1.SetIconPadding(this.textBox1, -20);
            this.errorProvider1.SetIconPadding(this.textBox2, -20);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(_viewModel.Error))
            {
                MessageBox.Show("The PIN cannot be changed because there is a problem with the information entered.\n\n" + _viewModel.Error);
                return;
            }

            try
            {
                _viewModel.Commit();
                MessageBox.Show(String.Format("The PIN for '{0}' was successfully changed.\n\n", this.UserPrincipal.Name),
                    String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("The PIN for '{0}' could not be changed.\n\n{1}", this.UserPrincipal.Name, ex.Message),
                    String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class ResetPinViewModel : EditableViewModel<UserPrincipal>, IDataErrorInfo
        {
            private string _newPin;
            private string _confirmPin;

            public string NewPin
            {
                get { return _newPin; }
                set
                {
                    if (_newPin != value)
                    {
                        _newPin = value;
                        NotifyPropertyChanged("NewPin");
                    }
                }
            }

            public string ConfirmPin
            {
                get { return _confirmPin; }
                set
                {
                    if (_confirmPin != value)
                    {
                        _confirmPin = value;
                        NotifyPropertyChanged("ConfirmPin");
                    }
                }
            }

            public PogsDatabase Database { get; private set; }

            public ResetPinViewModel(UserPrincipal user, PogsDatabase database)
                : base(user)
            {
                this.Database = database;
            }

            public static readonly Regex VALID_PIN = new Regex(@"^[0-9]{4}$");

            public override void Commit()
            {
                if (!VALID_PIN.IsMatch(this.NewPin ?? String.Empty))
                    throw new InvalidOperationException("The PIN entered is not a 4-digit number.  (No other characters are allowed.)");
                if (this.NewPin != this.ConfirmPin)
                    throw new InvalidOperationException("The PINs entered do not match.");

                this.Database.ResetUserPin(this.Model, this.NewPin);
            }

            #region IDataErrorInfo Members

            public string Error
            {
                get
                {
                    if (!String.IsNullOrEmpty(this["NewPin"]))
                        return this["NewPin"];
                    else if (!String.IsNullOrEmpty(this["ConfirmPin"]))
                        return this["ConfirmPin"];
                    return String.Empty;
                }
            }

            public string this[string columnName]
            {
                get
                {
                    switch (columnName)
                    {
                        case "NewPin":
                            if (!String.IsNullOrEmpty(this.NewPin) && !VALID_PIN.IsMatch(this.NewPin))
                                return "The PIN entered must be a 4-digit number. (No other characters are allowed.)";
                            break;

                        case "ConfirmPin":

                            //only bother the user once they start typing
                            if (!String.IsNullOrEmpty(this.NewPin) && (!String.IsNullOrEmpty(this.ConfirmPin) && this.NewPin != this.ConfirmPin))
                                return "The PIN entered does not match the PIN entered above.";
                            break;
                    }

                    return String.Empty;
                }
            }

            #endregion IDataErrorInfo Members
        }
    }
}