﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogs.DataModel.Security
{
    public class SecurityPrincipalEventArgs : EventArgs
    {
        public SecurityPrincipal SecurityPrincipal { get; set; }

        public SecurityPrincipalEventArgs(SecurityPrincipal principal)
        {
            this.SecurityPrincipal = principal;
        }
    }
}
