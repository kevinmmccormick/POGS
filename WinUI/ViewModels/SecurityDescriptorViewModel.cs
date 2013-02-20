using System;
using System.Drawing;
using Pogs.DataModel.Security;

namespace Pogs.ViewModels
{
    public class SecurityDescriptorViewModel : EditableViewModel<SecurityDescriptor>
    {
        private SecurityPrincipalViewModel _principalViewModel;

        internal SecurityDescriptor Descriptor
        {
            get { return this.Model; }
        }

        public Image Icon
        {
            get { return _principalViewModel.TypeImage; }
        }

        public string Name
        {
            get { return _principalViewModel.Name; }
        }

        public bool IsEditor
        {
            get { return this.Model.EditingAllowed; }
            set
            {
                if (this.IsEditor == value)
                    return;

                this.Model = new SecurityDescriptor(this.Model.SecurityPrincipal, this.IsViewer || value, value);
                NotifyPropertyChanged("IsViewer");
                NotifyPropertyChanged("IsEditor");
            }
        }

        public bool IsViewer
        {
            get { return this.Model.ViewAllowed; }
            set
            {
                if (this.IsViewer == value)
                    return;

                this.Model = new SecurityDescriptor(this.Model.SecurityPrincipal, value, this.IsEditor && value);
                NotifyPropertyChanged("IsViewer");
                NotifyPropertyChanged("IsEditor");
            }
        }

        public SecurityDescriptorViewModel(SecurityDescriptor descriptor)
            : base(descriptor)
        {
            _principalViewModel = new SecurityPrincipalViewModel(descriptor.SecurityPrincipal);
        }

        protected override void Disposing(bool disposing)
        {
            _principalViewModel.Dispose();

            base.Disposing(disposing);
        }

        public override void Commit()
        {
            throw new NotImplementedException();
        }
    }
}