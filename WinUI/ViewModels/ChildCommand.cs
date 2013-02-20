using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Pogs.ViewModels
{
    public abstract class ChildCommand<TParent> : ICommand where TParent : INotifyPropertyChanged
    {
        /// <summary>
        /// A list of property names that, when the PropertyChanged event is raised,
        /// should indicate that this object should re-evaluate the CanExecute property's value
        /// and raise the CanExecuteChanged event if necessary.
        /// </summary>
        protected IList<string> DependentProperties { get; private set; }

        public event EventHandler CanExecuteChanged;

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, e);
            }
        }

        protected TParent Parent { get; private set; }

        public ChildCommand(TParent parent)
        {
            this.Parent = parent;
            this.DependentProperties = new List<string>();
            this.Parent.PropertyChanged += new PropertyChangedEventHandler(Parent_PropertyChanged);
        }

        private void Parent_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.DependentProperties.Contains(e.PropertyName))
                OnCanExecuteChanged(EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteCore();
        }

        protected abstract bool CanExecuteCore();

        public void Execute(object parameter)
        {
            ExecuteCore();
        }

        protected abstract void ExecuteCore();

        /// <summary>
        /// A method that is called to notify this object to raise the CanExecuteChanged event.
        /// </summary>
        public void NotifyCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }
    }
}