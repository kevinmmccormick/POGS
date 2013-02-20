using System;
using System.ComponentModel;

namespace Pogs.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public abstract class ViewModel<T> : ViewModel, IDisposable where T : class
    {
        protected T Model { get; set; }

        public ViewModel(T model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            this.Model = model;

            RegisterModelEvents();
        }

        protected virtual void RegisterModelEvents()
        { }

        protected virtual void UnregisterModelEvents()
        { }

        #region IDisposable Members

        public void Dispose()
        {
            Disposing(true);
        }

        protected virtual void Disposing(bool disposing)
        {
            if (disposing)
            {
                UnregisterModelEvents();
            }
        }

        #endregion
    }

    public abstract class EditableViewModel<T> : ViewModel<T> where T : class
    {
        public EditableViewModel(T model)
            : base(model)
        { }

        public abstract void Commit();
    }
}