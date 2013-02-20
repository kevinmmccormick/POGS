using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Pogs.DataModel.Security
{
    public class SecurityDescriptorCollection : Collection<SecurityDescriptor>
    {
        public SecurityDescriptorCollection Parent { get; set; }

        /// <summary>
        /// Gets or sets the object that this SecurityDescriptorCollection provides descriptors for.
        /// </summary>
        public object Owner { get; set; }

        public bool CheckIsAllowedView(SecurityPrincipal principal)
        {
            if (principal.IsAdminRecursive)
                return true;

            if (this.Count == 0)
            {
                if (this.Parent == null)
                    return false;
                else
                    return this.Parent.CheckIsAllowedView(principal);
            }

            foreach (var descriptor in this)
            {
                if (descriptor.ViewAllowed && descriptor.SecurityPrincipal == principal)
                    return true;

                if (descriptor.ViewAllowed && descriptor.SecurityPrincipal is GroupPrincipal && ((GroupPrincipal)descriptor.SecurityPrincipal).ContainsMemberRecursive(principal))
                    return true;
            }

            return false;
        }

        public bool CheckIsAllowedEdit(SecurityPrincipal principal)
        {
            if (principal.IsAdminRecursive)
                return true;

            if (this.Count == 0)
            {
                if (this.Parent == null)
                    return false;
                else
                    return this.Parent.CheckIsAllowedEdit(principal);
            }

            foreach (var descriptor in this)
            {
                if (descriptor.SecurityPrincipal == principal && descriptor.EditingAllowed)
                    return true;

                if (descriptor.EditingAllowed && descriptor.SecurityPrincipal is GroupPrincipal && ((GroupPrincipal)descriptor.SecurityPrincipal).ContainsMember(principal))
                    return true;
            }

            return false;
        }
    }
}