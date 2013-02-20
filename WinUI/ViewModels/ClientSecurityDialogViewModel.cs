using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Pogs.DataModel;

namespace Pogs.ViewModels
{
    public class ClientSecurityDialogViewModel : EditableViewModel<ClientRecord>
    {
        private bool _useDefaultSecurity;
        private IEnumerable<SecurityDescriptorViewModel> _cachedCustom;

        public bool UseDefaultSecurity
        {
            get { return _useDefaultSecurity; }
            set
            {
                if (_useDefaultSecurity == value)
                    return;

                _useDefaultSecurity = value;

                //hide anything selected to let the box appear empty
                if (value)
                {
                    _cachedCustom = this.Descriptors.Descriptors.ToList();
                    this.Descriptors.Descriptors.Clear();
                }
                else if (_cachedCustom != null)
                {
                    foreach (var cache in _cachedCustom)
                        this.Descriptors.Descriptors.Add(cache);

                    _cachedCustom = null;
                }

                NotifyPropertyChanged("UseDefaultSecurity");
                NotifyPropertyChanged("DescriptorsEditable");
            }
        }

        public string Title
        {
            get { return String.Format("{0} Security", this.Model.Name); }
        }

        public SecurityEditorViewModel Descriptors { get; set; }

        public bool DescriptorsEditable
        {
            get { return !this.UseDefaultSecurity; }
        }

        public ICommand SaveCommand { get; private set; }

        internal PogsDatabase Database { get; private set; }

        public ClientSecurityDialogViewModel(ClientRecord client, PogsDatabase database)
            : base(client)
        {
            this.Descriptors = new SecurityEditorViewModel(client.Security, database.SecurityPrincipals);
            this.SaveCommand = new SaveDescriptorsCommand(this);
            this.Database = database;

            this.UseDefaultSecurity = client.Security.Count == 0;
        }

        public override void Commit()
        {
            var original = this.Model.Security.ToList();

            try
            {
                if (this.UseDefaultSecurity)
                {
                    this.Model.Security.Clear();
                }
                else
                {
                    this.Descriptors.Commit();
                }

                this.Database.CommitClientSecurity(this.Model);
            }
            catch
            {
                this.Model.Security.Clear();
                throw;
            }
        }

        public class SaveDescriptorsCommand : ChildCommand<ClientSecurityDialogViewModel>, IDialogResultCommand
        {
            public DialogResult LastResult { get; private set; }

            public SaveDescriptorsCommand(ClientSecurityDialogViewModel parent)
                : base(parent)
            {
                this.DependentProperties.Add("UseDefaultSecurity");
                this.LastResult = DialogResult.None;
                this.Parent.Descriptors.Descriptors.ListChanged += new ListChangedEventHandler(Descriptors_ListChanged);
            }

            private void Descriptors_ListChanged(object sender, ListChangedEventArgs e)
            {
                OnCanExecuteChanged(EventArgs.Empty);
            }

            protected override bool CanExecuteCore()
            {
                return this.Parent.UseDefaultSecurity || this.Parent.Descriptors.Descriptors.Any();
            }

            protected override void ExecuteCore()
            {
                try
                {
                    this.Parent.Commit();
                    this.LastResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("The security for this client could not be updated.\n\n{0}", ex.Message));
                    this.LastResult = DialogResult.None;
                }
            }
        }
    }
}