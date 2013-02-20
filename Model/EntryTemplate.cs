using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogs.DataModel
{
    public class EntryTemplate
    {
        #region Declarations

        private int _iconIndex;
        private List<EntryField> _fields = new List<EntryField>();
        private string _name;

        #endregion

        /// <summary>
        /// The Name of the Entry Template, such as "Bank Account".
        /// </summary>
        public string Name 
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnNameChanged(EventArgs.Empty);
                }
            }
        }

        public int IconIndex 
        {
            get { return _iconIndex; }
            set
            {
                if (_iconIndex != value)
                {
                    _iconIndex = value;
                    OnIconIndexChanged(EventArgs.Empty);
                }
            }
        }

        public IEnumerable<EntryField> Fields 
        {
            get { return _fields.AsReadOnly(); }
        }

        public bool AllowCustomFields { get; set; }

        public event EventHandler NameChanged;
        protected void OnNameChanged(EventArgs e)
        {
            if (NameChanged != null)
            {
                NameChanged(this, e);
            }
        }

        public event EventHandler IconIndexChanged;
        protected void OnIconIndexChanged(EventArgs e)
        {
            if (IconIndexChanged != null)
            {
                IconIndexChanged(this, e);
            }
        }

        public event EventHandler<EntryFieldEventArgs> FieldAdded;
        protected void OnFieldAdded(EntryFieldEventArgs e)
        {
            if (FieldAdded != null)
            {
                FieldAdded(this, e);
            }
        }

        public event EventHandler<EntryFieldEventArgs> FieldRemoved;
        protected void OnFieldRemoved(EntryFieldEventArgs e)
        {
            if (FieldRemoved != null)
            {
                FieldRemoved(this, e);
            }
        }

        public void AddField(EntryField templateField)
        {
            if (templateField == null)
                throw new ArgumentNullException("templateField");
            if (_fields.Contains(templateField))
                throw new ArgumentException("This template already contains this field.");
            if (templateField.Container != null)
                throw new ArgumentException("This template is already contained in a field container.");

            _fields.Add(templateField);

            templateField.Container = this;

            OnFieldAdded(new EntryFieldEventArgs(templateField));
        }

        public void RemoveField(EntryField field)
        {
            if (field == null)
                throw new ArgumentNullException("field");
            if (!_fields.Contains(field))
                throw new ArgumentException("This template does not contain the specified field.");

            _fields.Remove(field);

            OnFieldRemoved(new EntryFieldEventArgs(field));
        }
    }
}
