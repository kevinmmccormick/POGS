namespace Pogs.DataModel
{
    public class EntryField
    {
        public string Name { get; set; }

        public EntryFieldType EntryType { get; set; }

        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the object that manages this EntryField.
        /// Is either a Template or an Entry (if it is a custom field).
        /// </summary>
        public object Container { get; set; }
    }
}