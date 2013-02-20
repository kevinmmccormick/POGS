using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pogs.Views
{
    /// <summary>
    /// A Form that manages databinding between a DataContext (ViewModel) and a BindingSource.
    /// </summary>
    [Description("A Form that manages databinding between a DataContext (ViewModel) and a BindingSource.")]
    public class MvvmForm : Form
    {
        private object _dataContext;
        private BindingSource _mainBindingSource;

        /// <summary>
        /// Gets or sets an object (usually a ViewModel) that will be bound to the MainBindingSource that is defined for this Form.
        /// </summary>
        [Description("An object (usually a ViewModel) that will be bound to the MainBindingSource that is defined for this Form.")]
        [DefaultValue(null)]
        [Bindable(BindableSupport.Yes)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object DataContext
        {
            get { return _dataContext; }
            set
            {
                if (this.IsDisposed)
                    throw new ObjectDisposedException(this.GetType().Name);
                if (_dataContext == value)
                    return;

                OnDataContextChanging();

                if (_mainBindingSource != null && !(_mainBindingSource.DataSource is Type))
                    _mainBindingSource.DataSource = ListBindingHelper.GetListItemType(_mainBindingSource);

                _dataContext = value;

                if (_dataContext != null && _mainBindingSource != null)
                    _mainBindingSource.DataSource = _dataContext;

                OnDataContextChanged();
            }
        }

        protected virtual void OnDataContextChanging()
        { }

        protected virtual void OnDataContextChanged()
        { }

        [DefaultValue(null)]
        public BindingSource MainBindingSource
        {
            get { return _mainBindingSource; }
            set
            {
                if (_mainBindingSource == value)
                    return;

                if (_mainBindingSource != null && !(_mainBindingSource.DataSource is Type))
                    _mainBindingSource.DataSource = ListBindingHelper.GetListItemType(_mainBindingSource);

                _mainBindingSource = value;

                if (_dataContext != null && _mainBindingSource != null)
                    _mainBindingSource.DataSource = _dataContext;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DataContext = null;
            }

            base.Dispose(disposing);
        }
    }
}