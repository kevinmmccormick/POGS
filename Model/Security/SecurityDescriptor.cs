using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pogs.DataModel.Security;

namespace Pogs.DataModel.Security
{
    public class SecurityDescriptor
    {
        public SecurityPrincipal SecurityPrincipal { get; set; }

        public bool ViewAllowed { get; private set; }
        public bool EditingAllowed { get; private set; }

        public SecurityDescriptor(SecurityPrincipal principal, bool view, bool edit)
        {
            this.SecurityPrincipal = principal;
            this.ViewAllowed = view;
            this.EditingAllowed = edit;
        }

        public static bool operator ==(SecurityDescriptor a, SecurityDescriptor b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
                return true;

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
                return false;

            // Return true if the fields match:
            return a.SecurityPrincipal == b.SecurityPrincipal &&
                   a.ViewAllowed == b.ViewAllowed &&
                   a.EditingAllowed == b.EditingAllowed;
        }

        public static bool operator !=(SecurityDescriptor a, SecurityDescriptor b)
        {
            return !(a == b);
        }

        protected SecurityDescriptor(SecurityDescriptor original)
        {
            this.SecurityPrincipal = original.SecurityPrincipal;
            this.EditingAllowed = original.EditingAllowed;
            this.ViewAllowed = original.ViewAllowed;
        }

        public SecurityDescriptor Clone()
        {
            return new SecurityDescriptor(this);
        }
    }
}
