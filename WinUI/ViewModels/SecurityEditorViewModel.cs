using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using Pogs.DataModel;
using Pogs.DataModel.Security;
using Pogs.PogsMain;

namespace Pogs.ViewModels
{
    public class SecurityEditorViewModel : EditableViewModel<SecurityDescriptorCollection>
    {
        private SecurityDescriptorViewModel _selected;

        public SecurityDescriptorViewModel SelectedDescriptor
        {
            get { return _selected; }
            set
            {
                if (_selected == value)
                    return;

                _selected = value;
                ((RemoveSelectedDescriptorCommand)this.RemoveSelectedDescriptor).NotifyCanExecuteChanged();
            }
        }

        public BindingList<SecurityDescriptorViewModel> Descriptors { get; private set; }

        public ICommand AddDescriptor { get; private set; }

        public ICommand RemoveSelectedDescriptor { get; private set; }

        public IEnumerable<SecurityPrincipal> Principals { get; private set; }

        public SecurityEditorViewModel(SecurityDescriptorCollection descriptors, IEnumerable<SecurityPrincipal> principals)
            : base(descriptors)
        {
            this.Principals = principals;
            this.Descriptors = new BindingList<SecurityDescriptorViewModel>();
            this.AddDescriptor = new AddDescriptorCommand(this);
            this.RemoveSelectedDescriptor = new RemoveSelectedDescriptorCommand(this);

            foreach (var descriptor in descriptors.Select(d => new SecurityDescriptorViewModel(d.Clone())))
                this.Descriptors.Add(descriptor);

            _original = descriptors.Select(d => d.Clone()).ToList();
        }

        private List<SecurityDescriptor> _original;

        public override void Commit()
        {
            this.Model.Clear();
            foreach (var descriptor in this.Descriptors)
                this.Model.Add(descriptor.Descriptor);
        }

        internal void UndoCommit()
        {
            this.Model.Clear();
            foreach (var descriptor in _original)
                this.Model.Add(descriptor);
        }

        public class AddDescriptorCommand : ChildCommand<SecurityEditorViewModel>
        {
            public AddDescriptorCommand(SecurityEditorViewModel parent)
                : base(parent)
            { }

            protected override bool CanExecuteCore()
            {
                return true;
            }

            protected override void ExecuteCore()
            {
                using (var dialogViewModel = new SecurityPrincipalSelectorViewModel(this.Parent.Principals))
                using (var dialog = new SecurityPrincipalSelectorDialog { DataContext = dialogViewModel })
                {
                    if (dialog.ShowDialog() == DialogResult.OK && dialogViewModel.Selected != null)
                    {
                        foreach (var selected in dialogViewModel.Selected)
                        {
                            if (!this.Parent.Descriptors.Any(d => d.Descriptor.SecurityPrincipal == selected.SecurityPrincipal))
                                this.Parent.Descriptors.Add(new SecurityDescriptorViewModel(new SecurityDescriptor(selected.SecurityPrincipal, true, false)));
                        }
                    }
                }
            }
        }

        public class RemoveSelectedDescriptorCommand : ChildCommand<SecurityEditorViewModel>
        {
            public RemoveSelectedDescriptorCommand(SecurityEditorViewModel parent)
                : base(parent)
            { }

            protected override bool CanExecuteCore()
            {
                return this.Parent.SelectedDescriptor != null;
            }

            protected override void ExecuteCore()
            {
                this.Parent.Descriptors.Remove(this.Parent.SelectedDescriptor);
            }
        }
    }
}