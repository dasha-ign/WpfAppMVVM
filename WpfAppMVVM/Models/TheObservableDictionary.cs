using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace WpfAppMVVM.WPF.Models
{
    public class TheObservableDictionary<TKey,TValue> : ObservableCollection<DictionaryEntry<TKey,TValue>>
    {
        public TheObservableDictionary()
            : base()
        {
        }

        //public TheObservableDictionary(IEnumerable<DictionaryEntry<TKey, TValue>> enumerable)
        //    : base(enumerable)
        //{
        //}

        //public TheObservableDictionary(List<DictionaryEntry<TKey, TValue>> list)
        //    : base(list)
        //{
        //}

        public TheObservableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            ArgumentNullException.ThrowIfNull(dictionary, nameof(dictionary));
            foreach (var kv in dictionary)
            {
                Add(new DictionaryEntry<TKey, TValue>(kv.Key,kv.Value));
            }
        }

        public TValue this[TKey key]
        {
            get => (this.Where(item => item.EntryKey.Equals(key)).FirstOrDefault() != null) ?
                    this.Where(item => item.EntryKey.Equals(key)).FirstOrDefault().EntryValue :
                    default;
            set => throw new NotImplementedException();
        }



        public ICollection<TKey> Keys
        {
            get
            {
                ObservableCollection<TKey> result = new();
                foreach (var kv in this)
                {
                    result.Add(kv.EntryKey);
                }
                return result;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                ObservableCollection<TValue> result = new();
                foreach (var kv in this)
                {
                    result.Add(kv.EntryValue);
                }
                return result;
            }
        }


        public ICollection<string> Descriptions
        {
            get
            {
                ObservableCollection<string> result = new();
                foreach (var kv in this)
                    result.Add(kv.EntryDescription);

                return result;
            }
        }


        public bool ContainsValue(TValue value) => this.Values.Contains(value);

        public bool IsReadOnly => false;

        public Type ElementType => typeof(DictionaryEntry<TKey, TValue>);

        public void Add(TKey key, TValue value) => base.Add(new(key, value));

        public void Add(DictionaryEntry<TKey, TValue> item) => base.Add(item);

        public bool Contains(DictionaryEntry<TKey, TValue> item) => base.Contains(item);

        public bool ContainsKey(TKey key) => this.Keys.Contains(key);

        public void CopyTo(DictionaryEntry<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }


        public bool Remove(DictionaryEntry<TKey, TValue> item) => base.Remove(item);

        

        public new void Clear() => base.Clear();

        public bool TryAdd(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (!this.ContainsKey(key))
            {
                var item = new DictionaryEntry<TKey, TValue>(key, value);
                Add(item);
                if (this.Contains(item))
                    return true;
            }

            return false;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            value = this[key];
            return !value.Equals(default(TValue));
        }

        public async void SaveToJsonFile(string fileName)
        {
            using FileStream stream = File.Create(fileName);
            var dictitonary = this.ToDictionary();
            await System.Text.Json.JsonSerializer.SerializeAsync<Dictionary<TKey, TValue>>(stream,
                                            dictitonary,
                                            options: new JsonSerializerOptions
                                            {
                                                WriteIndented = true,
                                                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                                                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin,
                                                                                    UnicodeRanges.Cyrillic)
                                            }
                                        );
            await stream.DisposeAsync();
        }



        public Dictionary<TKey, TValue> ToDictionary()
        {
            Dictionary<TKey, TValue> result = new();
            foreach (var kv in this)
            {
                result.Add(kv.EntryKey, kv.EntryValue);
            }
            return result;
        }


    }
}
