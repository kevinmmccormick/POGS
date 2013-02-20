using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pogs.DataModel;
using Pogs.DataModel.Security;

namespace Pogs.ViewModels
{
    public class SecurityPrincipalSelectorViewModel : ViewModel<IEnumerable<SecurityPrincipal>>
    {
        private IEnumerable<SecurityPrincipalViewModel> _selected;

        public IDialogResultCommand Ok { get; private set; }

        public BindingList<SecurityPrincipalViewModel> Principals { get; private set; }

        public IEnumerable<SecurityPrincipalViewModel> Selected
        {
            get { return _selected; }
            set
            {
                if (_selected == value ||
                    (_selected != null && value != null && Enumerable.SequenceEqual(_selected, value)))
                    return;

                _selected = value;
                ((OkCommand)this.Ok).NotifyCanExecuteChanged();
            }
        }

        public SecurityPrincipalSelectorViewModel(IEnumerable<SecurityPrincipal> principals)
            : base(principals)
        {
            this.Principals = new BindingList<SecurityPrincipalViewModel>();

            foreach (var principal in principals.OrderBy(p => p.Name).Select(p => new SecurityPrincipalViewModel(p)))
                this.Principals.Add(principal);

            this.Ok = new OkCommand(this);
        }

        public class OkCommand : ChildCommand<SecurityPrincipalSelectorViewModel>, IDialogResultCommand
        {
            public DialogResult LastResult { get; private set; }

            public OkCommand(SecurityPrincipalSelectorViewModel parent)
                : base(parent)
            { }

            protected override bool CanExecuteCore()
            {
                return this.Parent.Selected != null &&
                    this.Parent.Selected.Any();
            }

            protected override void ExecuteCore()
            {
                this.LastResult = DialogResult.OK;
            }
        }
    }
}