using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogs.DataModel.Security
{
    public class SecurityPrincipalsEventArgs : EventArgs
    {
        public IEnumerable<SecurityPrincipal> SecurityPrincipals { get; set; }

        public SecurityPrincipalsEventArgs(IEnumerable<SecurityPrincipal> principals)
        {
            this.SecurityPrincipals = principals;
        }

        public SecurityPrincipalsEventArgs(SecurityPrincipal principal)
        {
            this.SecurityPrincipals = new[] { principal };
        }
    }
}
