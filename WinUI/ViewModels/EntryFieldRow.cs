using Pogs.DataModel;

namespace Pogs.VisualModel
{
    /// <summary>
    /// An editable display of an EntryField and value.
    /// </summary>
    public class EntryFieldRow
    {
        public EntryField SourceField { get; private set; }

        public ClientEntry SourceEntry { get; private set; }

        public string FieldName { get; set; }

        public string Value { get; set; }

        public bool IsCustom
        {
            get { return this.SourceField.Container is ClientEntry; }
        }

        public EntryFieldRow(ClientEntry sourceEntry, EntryField sourceField)
        {
            this.SourceEntry = sourceEntry;
            this.SourceField = sourceField;

            Reset();
        }

        public void Reset()
        {
            this.FieldName = this.SourceField.Name;
            this.Value = this.SourceEntry.GetValue(this.SourceField);
        }

        /// <summary>
        /// Updates the data model with the data in the UI.
        /// </summary>
        /// <returns>True if any data was changed in the UI.</returns>
        public bool Commit()
        {
            if (this.SourceEntry.GetValue(this.SourceField) != this.Value)
            {
                this.SourceEntry.SetValue(this.SourceField, this.Value);
                return true;
            }

            return false;
        }
    }
}