using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pogs.DataModel;

namespace Pogs.VisualModel
{
    internal class TemplateView : IDisposable, INotifyPropertyChanged
    {
        private BindingList<EntryFieldView> _fieldViews;

        public BindingList<EntryFieldView> FieldViews
        {
            get
            {
                if (_fieldViews == null)
                {
                    _fieldViews = new BindingList<EntryFieldView>();

                    foreach (var field in this.Template.Fields)
                    {
                        _fieldViews.Add(new EntryFieldView(field));
                    }
                }

                return _fieldViews;
            }
        }

        public EntryTemplate Template { get; private set; }

        public bool AllowCustomFields { get; set; }

        public string Name { get; set; }

        public Image Image
        {
            get
            {
                if (this.ImageList == null)
                    return null;

                return this.ImageList.Images[this.IconIndex];
            }
        }

        private int _iconIndex;

        public int IconIndex
        {
            get { return _iconIndex; }
            set
            {
                _iconIndex = value;
                NotifyPropertyChanged("IconIndex");
                NotifyPropertyChanged("Image");
            }
        }

        public ImageList ImageList { get; set; }

        public TemplateView(EntryTemplate template)
        {
            if (template == null)
                throw new ArgumentNullException("template");

            this.Name = template.Name;
            this.AllowCustomFields = template.AllowCustomFields;
            this.IconIndex = template.IconIndex;

            this.Template = template;

            RegisterDataModelEvents();
        }

        private void RegisterDataModelEvents()
        {
            this.Template.NameChanged += new EventHandler(Template_NameChanged);
            this.Template.IconIndexChanged += new EventHandler(Template_IconIndexChanged);
            this.Template.FieldAdded += new EventHandler<EntryFieldEventArgs>(Template_FieldAdded);
            this.Template.FieldRemoved += new EventHandler<EntryFieldEventArgs>(Template_FieldRemoved);
        }

        private void UnregisterDataModelEvents()
        {
            this.Template.NameChanged -= Template_NameChanged;
            this.Template.IconIndexChanged -= Template_IconIndexChanged;
            this.Template.FieldAdded -= Template_FieldAdded;
            this.Template.FieldRemoved -= Template_FieldRemoved;
        }

        private void Template_IconIndexChanged(object sender, EventArgs e)
        {
            NotifyPropertyChanged("Image");
        }

        private void Template_NameChanged(object sender, EventArgs e)
        {
            NotifyPropertyChanged("Name");
        }

        private void Template_FieldRemoved(object sender, EntryFieldEventArgs e)
        {
            if (_fieldViews != null)
            {
                var view = _fieldViews.FirstOrDefault(v => v.Field == e.EntryField);
                if (view != null)
                    _fieldViews.Remove(view);
            }
        }

        private void Template_FieldAdded(object sender, EntryFieldEventArgs e)
        {
            if (_fieldViews != null)
            {
                _fieldViews.Add(new EntryFieldView(e.EntryField));
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnregisterDataModelEvents();
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
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

        internal bool Commit()
        {
            if (this.Name != this.Template.Name)
                this.Template.Name = this.Name;
            if (this.IconIndex != this.Template.IconIndex)
                this.Template.IconIndex = this.IconIndex;
            if (this.AllowCustomFields != this.Template.AllowCustomFields)
                this.Template.AllowCustomFields = this.AllowCustomFields;

            if (_fieldViews != null)
            {
                this.Template.FieldAdded -= Template_FieldAdded;

                foreach (var v in _fieldViews)
                {
                    v.Commit();

                    if (v.Field.Container == null)
                    {
                        this.Template.AddField(v.Field);
                    }
                }

                this.Template.FieldAdded += Template_FieldAdded;

                //remove deleted fields
                foreach (var field in this.Template.Fields.Except(_fieldViews.Select(fv => fv.Field)).ToList())
                {
                    this.Template.RemoveField(field);
                }
            }

            return true;
        }

        /// <summary>
        /// Adds a new FieldView to the templateview for a field
        /// that is not committed to the datamodel yet.
        /// </summary>
        /// <returns></returns>
        internal EntryFieldView AddNewField()
        {
            var view = new EntryFieldView(new EntryField());
            _fieldViews.Add(view);
            return view;
        }

        internal void RemoveView(EntryFieldView fieldView)
        {
            _fieldViews.Remove(fieldView);
        }
    }
}