using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Pogs.DataModel.Security
{
    public abstract class SecurityPrincipal
    {
        private string _name;
        private bool _isAdmin;

        public string Name 
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnNameChanged(EventArgs.Empty);
                }
            }
        }

        public bool IsAdmin 
        {
            get { return _isAdmin; }
            set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                    OnIsAdminChanged(EventArgs.Empty);
                }
            }
        }

        public bool IsAdminRecursive
        {
            get { return _isAdmin || this.Memberships.Any(g => g.IsAdmin); }
        }

        /// <summary>
        /// Event that is raised when the value of the Name property has changed.
        /// </summary>
        public event EventHandler NameChanged;
        protected virtual void OnNameChanged(EventArgs e)
        {
            if (NameChanged != null)
            {
                NameChanged(this, e);
            }
        }

        /// <summary>
        /// Event that is raised when the value of the IsAdmin property has changed.
        /// </summary>
        public event EventHandler IsAdminChanged;
        protected virtual void OnIsAdminChanged(EventArgs e)
        {
            if (IsAdminChanged != null)
            {
                IsAdminChanged(this, e);
            }
        }

        internal IList<GroupPrincipal> Memberships { get; private set; }

        public SecurityPrincipal()
        {
            this.Memberships = new List<GroupPrincipal>();
        }
    }
}
