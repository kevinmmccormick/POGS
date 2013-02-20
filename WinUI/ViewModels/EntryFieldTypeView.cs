using System;
using System.ComponentModel;
using Pogs.DataModel;

namespace Pogs.VisualModel
{
    public class EntryFieldTypeView
    {
        private static BindingList<EntryFieldTypeView> _typeView;

        public string Label { get; private set; }

        public EntryFieldType EntryFieldType { get; private set; }

        public EntryFieldTypeView(string label, EntryFieldType type)
        {
            if (String.IsNullOrEmpty(label))
                throw new ArgumentNullException("label");

            this.Label = label;
            this.EntryFieldType = type;
        }

        public static BindingList<EntryFieldTypeView> All
        {
            get
            {
                if (_typeView == null)
                {
                    _typeView = new BindingList<EntryFieldTypeView>();

                    _typeView.Add(new EntryFieldTypeView("Plain Text", EntryFieldType.PlainText));
                    _typeView.Add(new EntryFieldTypeView("Password", EntryFieldType.Password));
                    _typeView.Add(new EntryFieldTypeView("Web Site", EntryFieldType.Website));
                    _typeView.Add(new EntryFieldTypeView("Phone Number", EntryFieldType.PhoneNumber));
                    _typeView.Add(new EntryFieldTypeView("Date", EntryFieldType.Date));
                    _typeView.Add(new EntryFieldTypeView("Credit Card", EntryFieldType.CreditCardNumber));
                }

                return _typeView;
            }
        }
    }
}