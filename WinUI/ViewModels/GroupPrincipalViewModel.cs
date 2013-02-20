using System;
using System.ComponentModel;
using System.Linq;
using Pogs.DataModel.Security;

namespace Pogs.ViewModels
{
    public class GroupPrincipalViewModel : SecurityPrincipalViewModel
    {
        private BindingList<SecurityPrincipalViewModel> _memberViewModels = new BindingList<SecurityPrincipalViewModel>();

        public BindingList<SecurityPrincipalViewModel> MemberViewModels
        {
            get { return _memberViewModels; }
        }

        internal new GroupPrincipal Model
        {
            get { return (GroupPrincipal)base.Model; }
        }

        internal GroupPrincipalViewModel(GroupPrincipal group)
            : base(group)
        {
            foreach (var member in group.Members)
            {
                _memberViewModels.Add(new SecurityPrincipalViewModel(member));
            }
        }

        private void GroupPrincipal_MemberRemoved(object sender, SecurityPrincipalEventArgs e)
        {
            var match = _memberViewModels.FirstOrDefault(spvm => spvm.SecurityPrincipal == e.SecurityPrincipal);
            if (match != null)
                _memberViewModels.Remove(match);
        }

        private void GroupPrincipal_MemberAdded(object sender, SecurityPrincipalEventArgs e)
        {
            var match = _memberViewModels.FirstOrDefault(spvm => spvm.SecurityPrincipal == e.SecurityPrincipal);
            if (match == null)
                _memberViewModels.Add(new SecurityPrincipalViewModel(e.SecurityPrincipal));
        }

        protected override void RegisterModelEvents()
        {
            base.RegisterModelEvents();

            this.Model.MemberAdded += new EventHandler<SecurityPrincipalEventArgs>(GroupPrincipal_MemberAdded);
            this.Model.MemberRemoved += new EventHandler<SecurityPrincipalEventArgs>(GroupPrincipal_MemberRemoved);
        }

        protected override void UnregisterModelEvents()
        {
            base.UnregisterModelEvents();

            this.Model.MemberAdded -= new EventHandler<SecurityPrincipalEventArgs>(GroupPrincipal_MemberAdded);
            this.Model.MemberRemoved -= new EventHandler<SecurityPrincipalEventArgs>(GroupPrincipal_MemberRemoved);
        }

        public override void Commit()
        {
            base.Commit();

            //TODO: modify the member list
        }
    }
}