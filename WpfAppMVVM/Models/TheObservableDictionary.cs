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
            get => base[Convert.ToInt32(key)].EntryValue;
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

        public bool ContainsKey(TKey key) => this.Keys.Contains(key);        

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
            if (value == null) return false;
            return !value.Equals(default(TValue));
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
