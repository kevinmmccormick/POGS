using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pogs.DataModel.Security;

namespace Pogs.Repository
{
    internal class GroupKeyCache : TrackingDatabaseKeyCache<GroupPrincipal>
    {
        protected override void EndTracking(GroupPrincipal item)
        {
            item.NameChanged -= new EventHandler(AnyItemChanged);
            item.MemberAdded -= AnyItemChanged;
            item.MemberRemoved -= AnyItemChanged;
        }

        protected override void BeginTracking(GroupPrincipal item)
        {
            item.NameChanged += new EventHandler(AnyItemChanged);
            item.MemberAdded += AnyItemChanged;
            item.MemberRemoved += AnyItemChanged;
        }

        private void AnyItemChanged(object sender, EventArgs e)
        {
            SetItemDirty((GroupPrincipal)sender);
        }
    }
}