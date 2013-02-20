using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pogs.DataModel.Security;

namespace Pogs.Repository
{
    internal class UserKeyCache : TrackingDatabaseKeyCache<UserPrincipal>
    {
        protected override void EndTracking(UserPrincipal item)
        {
            item.NameChanged -= AnyItemChanged;
            item.IsAdminChanged -= AnyItemChanged;
        }

        protected override void BeginTracking(UserPrincipal item)
        {
            item.NameChanged += AnyItemChanged;
            item.IsAdminChanged += AnyItemChanged;
        }

        private void AnyItemChanged(object sender, EventArgs e)
        {
            SetItemDirty((UserPrincipal)sender);
        }
    }
}