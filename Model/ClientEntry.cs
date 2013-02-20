using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Pogs.DataModel
{
    public class ClientEntry : INotifyPropertyChanged
    {
        #region Declarations

        private string _name;
        private string _notes;
        private int _iconIndex;

        private List<EntryField> _customFields = new List<EntryField>();          //definition of custom fields
        private Dictionary<EntryField, string> _values = new Dictionary<EntryField, string>();  //values for all fields

        private EntryTemplate _template;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the template that provided the initial fields for this entry.
        /// </summary>
        public EntryTemplate Template
        {
            get { return _template; }
            set
            {
                if (_template != value)
                {
                    if (_template != null)
                    {
                        foreach (var v in _values.Keys.Where(k => k.Container == _template).ToList())
                            _values.Remove(v);
                    }

                    _template = value;
                }
            }
        }

        public IEnumerable<EntryField> CustomFields
        {
            get { return _customFields.AsReadOnly(); }
        }

        public IEnumerable<EntryField> AllFields
        {
            get
            {
                return (this.Template == null ? Enumerable.Empty<EntryField>() : this.Template.Fields)
                    .Concat(this.CustomFields ?? Enumerable.Empty<EntryField>());
            }
        }

        public ClientRecord Client { get; set; }

        /// <summary>
        /// The name of this entry, such as "Joint Checking Account" or "Child's 529 Plan".
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// General notes related to this entry.
        /// </summary>
        public string Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    NotifyPropertyChanged("Notes");
                }
            }
        }

        /// <summary>
        /// The Icon specified for this entry, even if the template is omitted.
        /// </summary>
        public int IconIndex
        {
            get
            {
                if (this.Template != null)
                    return this.Template.IconIndex;
                else
                    return _iconIndex;
            }
            set
            {
                if (_iconIndex != value)
                {
                    _iconIndex = value;
                    NotifyPropertyChanged("IconIndex");
                }
            }
        }

        #endregion

        #region Events

        public event EventHandler<EntryFieldEventArgs> CustomFieldAdded;

        protected void OnCustomFieldAdded(EntryFieldEventArgs e)
        {
            if (CustomFieldAdded != null)
            {
                CustomFieldAdded(this, e);
            }
        }

        public event EventHandler<EntryFieldEventArgs> CustomFieldRemoved;

        protected void OnCustomFieldRemoved(EntryFieldEventArgs e)
        {
            if (CustomFieldRemoved != null)
            {
                CustomFieldRemoved(this, e);
            }
        }

        #endregion

        public void AddCustomField(EntryField field)
        {
            if (_customFields.Contains(field))
                throw new ArgumentException("The EntryField specified already exists in this ClientEntry.");
            if (field.Container != null)
                throw new ArgumentException("The EntryField specified is alredy contained within another container.");

            _customFields.Add(field);
            field.Container = this;

            OnCustomFieldAdded(new EntryFieldEventArgs(field));
        }

        public void RemoveCustomField(EntryField entryField)
        {
            if (!_customFields.Contains(entryField))
                throw new ArgumentException("The EntryField specified is not a custom field for this entry.");

            _customFields.Remove(entryField);
            entryField.Container = null;

            OnCustomFieldRemoved(new EntryFieldEventArgs(entryField));
        }

        /// <summary>
        /// Gets the value for a particular field for this entry.
        /// </summary>
        /// <param name="field">The custom field or template field to retrieve the value of.</param>
        /// <returns></returns>
        public string GetValue(EntryField field)
        {
            if (!_customFields.Contains(field) &&
                (this.Template == null || !this.Template.Fields.Contains(field)))
                throw new ArgumentException("The field specified is not part of this entry template, or is not one of this entry's custom fields.");

            if (!_values.ContainsKey(field))
            {
                _values.Add(field, field.DefaultValue ?? String.Empty);
            }

            return _values[field];
        }

        public void SetValue(EntryField field, string value)
        {
            if (!_customFields.Contains(field) &&
                (this.Template == null || !this.Template.Fields.Contains(field)))
                throw new ArgumentException("The field specified is not part of this entry template, or is not one of this entry's custom fields.");

            _values[field] = value;
        }

        public override string ToString()
        {
            return this.Name;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public string GetOrphanedValue(EntryField field)
        {
            string result;
            _values.TryGetValue(field, out result);
            return result;
        }
    }
}