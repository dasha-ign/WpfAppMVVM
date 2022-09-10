using System.Collections.Generic;

namespace WpfAppMVVM.WPF.Models
{
    public class DictionaryEntry<TKey,TValue>
    {
        public DictionaryEntry<TKey,TValue> Parent { get; set; }
        public TKey EntryKey { get; set; }
        public TValue EntryValue { get; set; }
        public string EntryDescription { get; set; }

        public List<DictionaryEntry<TKey,TValue>> Children { get; set; }

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

        public DictionaryEntry(){}

        public override string ToString()
        {
            string value = !string.IsNullOrEmpty(EntryDescription) ? EntryDescription : EntryValue?.ToString();
            return $"{EntryKey} : {value}";
        }
    }
}
