using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using Pogs.DataModel.Security;

namespace Pogs.ViewModels
{
    public class SecurityPrincipalViewModel : EditableViewModel<SecurityPrincipal>, IDataErrorInfo
    {
        private string _originalName;
        private bool _originalIsAdmin;

        private string _name;
        private bool? _isAdmin;

        private static readonly Image _userImage = new Icon(Pogs.WinUI.Properties.Resources.user, new Size(16, 16)).ToBitmap();
        private static readonly Image _usersImage = new Icon(Pogs.WinUI.Properties.Resources.users, new Size(16, 16)).ToBitmap();

        public Image TypeImage
        {
            get
            {
                if (this.Model is UserPrincipal)
                    return _userImage;
                if (this.Model is GroupPrincipal)
                    return _usersImage;

                return null;
            }
        }

        internal SecurityPrincipal SecurityPrincipal
        {
            get { return this.Model; }
        }

        public string TypeName
        {
            get
            {
                if (this.Model is UserPrincipal)
                    return "User";
                if (this.Model is GroupPrincipal)
                    return "Group";

                return "Unknown";
            }
        }

        public string Name
        {
            get
            {
                if (_name != null)
                    return _name;

                return this.Model.Name;
            }
            set
            {
                if (_name == value)
                    return;

                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public bool IsAdmin
        {
            get
            {
                if (_isAdmin.HasValue)
                    return _isAdmin.Value;

                return this.Model.IsAdmin;
            }
            set
            {
                if (this.IsAdmin == value)
                    return;

                _isAdmin = value;
                NotifyPropertyChanged("IsAdmin");
            }
        }

        public SecurityPrincipalViewModel(SecurityPrincipal principal)
            : base(principal)
        {
            _originalIsAdmin = principal.IsAdmin;
            _originalName = principal.Name;
        }

        private void Model_IsAdminChanged(object sender, EventArgs e)
        {
            _isAdmin = null;
            NotifyPropertyChanged("IsAdmin");
        }

        private void Model_NameChanged(object sender, EventArgs e)
        {
            _name = null;
            NotifyPropertyChanged("Name");
        }

        protected override void RegisterModelEvents()
        {
            this.Model.NameChanged += new EventHandler(Model_NameChanged);
            this.Model.IsAdminChanged += new EventHandler(Model_IsAdminChanged);
        }

        protected override void UnregisterModelEvents()
        {
            this.Model.NameChanged -= new EventHandler(Model_NameChanged);
            this.Model.IsAdminChanged -= new EventHandler(Model_IsAdminChanged);
        }

        public override void Commit()
        {
            this.Model.Name = this.Name;
            this.Model.IsAdmin = this.IsAdmin;
        }

        internal void UndoCommit()
        {
            string userEnteredName = this.Name;
            bool userEnteredAdmin = this.IsAdmin;

            this.Model.Name = _originalName;
            this.Model.IsAdmin = _originalIsAdmin;

            this.Name = userEnteredName;
            this.IsAdmin = userEnteredAdmin;
        }

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        if (this.Name == null || this.Name.Trim().Length == 0)
                        {
                            return "Name cannot be empty.";
                        }
                        break;
                }

                return null;
            }
        }

        #endregion IDataErrorInfo Members
    }
}