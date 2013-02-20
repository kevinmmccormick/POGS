using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogs.DataModel.Security
{
    public class GroupPrincipal : SecurityPrincipal
    {
        private List<SecurityPrincipal> _members = new List<SecurityPrincipal>();

        public IEnumerable<SecurityPrincipal> Members 
        {
            get { return _members; }
        }

        public event EventHandler<SecurityPrincipalEventArgs> MemberAdded;
        protected void OnMemberAdded(SecurityPrincipalEventArgs e)
        {
            if (MemberAdded != null)
            {
                MemberAdded(this, e);
            }
        }

        public event EventHandler<SecurityPrincipalEventArgs> MemberRemoved;
        protected void OnMemberRemoved(SecurityPrincipalEventArgs e)
        {
            if (MemberRemoved != null)
            {
                MemberRemoved(this, e);
            }
        }

        public void AddMember(SecurityPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException("principal");
            if (principal == this)
                throw new ArgumentException("A Group cannot be a member of itself.");
            if (_members.Contains(principal))
                throw new ArgumentException("This GroupPrincipal already contains the specified SecurityPrincipal.");

            _members.Add(principal);
            principal.Memberships.Add(this);

            OnMemberAdded(new SecurityPrincipalEventArgs(principal));
        }

        public void RemoveMember(SecurityPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException("principal");
            if (!_members.Contains(principal))
                throw new ArgumentException("The GroupPrincipal specified does not contain the specified SecurityPrincipal.");

            _members.Remove(principal);
            principal.Memberships.Remove(this);
            OnMemberRemoved(new SecurityPrincipalEventArgs(principal));
        }

        private static GroupPrincipal _everyone;

        public static GroupPrincipal Everyone
        {
            get
            {
                if (_everyone == null)
                    _everyone = new GroupPrincipal { Name = "Everyone" };

                return _everyone;
            }
        }

        public bool ContainsMember(SecurityPrincipal principal)
        {
            if (this == GroupPrincipal.Everyone)
                return true;
            else
                return _members.Contains(principal);
        }

        internal bool ContainsMemberRecursive(SecurityPrincipal principal)
        {
            if (ContainsMember(principal))
                return true;

            foreach (var subGroup in this.Members.OfType<GroupPrincipal>())
            {
                if (subGroup.ContainsMemberRecursive(principal))
                    return true;
            }

            return false;
        }
    }
}
