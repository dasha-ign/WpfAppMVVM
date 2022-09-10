using System.Collections.Generic;

namespace WpfAppMVVM.WPF.Models
{
    public class DictionaryEntry<TKey,TValue>
    {
        public TKey EntryKey { get; set; }
        public TValue EntryValue { get; set; } = default;
        public string EntryDescription { get; set; } = string.Empty;

        public DictionaryEntry(TKey entryKey, TValue entryValue, string entryDescription)
        {
            EntryKey = entryKey;
            EntryValue = entryValue;
            EntryDescription = entryDescription;
        }

        public DictionaryEntry(TKey entryKey, TValue entryValue)
        {
            EntryKey = entryKey;
            EntryValue = entryValue;
        }

        public DictionaryEntry(TKey entryKey)
        {
            EntryKey = entryKey;
        }

        public override string ToString()
        {
            string value = !string.IsNullOrEmpty(EntryDescription) ? EntryDescription : EntryValue?.ToString();
            return $"{EntryKey} : {value}";
        }
    }
}
