using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pogs.DataModel;

namespace Pogs.VisualModel
{
    internal class EntryFieldView
    {
        public string Name { get; set; }

        public EntryFieldType Type { get; set; }

        public string DefaultValue { get; set; }

        public EntryField Field { get; private set; }

        public EntryFieldView(EntryField field)
        {
            if (field == null)
                throw new ArgumentNullException("field");

            this.Field = field;

            this.Name = this.Field.Name;
            this.Type = this.Field.EntryType;
            this.DefaultValue = this.Field.DefaultValue;
        }

        public bool Commit()
        {
            if (this.Name != this.Field.Name)
                this.Field.Name = this.Name;
            if (this.Type != this.Field.EntryType)
                this.Field.EntryType = this.Type;
            if (this.DefaultValue != this.Field.DefaultValue)
                this.Field.DefaultValue = this.DefaultValue;

            return true;
        }
    }
}