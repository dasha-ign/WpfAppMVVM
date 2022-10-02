using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMVVM.WPF.Models
{
    public class TheDictionary<TKey,TValue>
    {
        public string Name { get; }

        public ObservableCollection<DictionaryEntry<TKey,TValue>> Content { get; set; }

        public TheDictionary(string name, ObservableCollection<DictionaryEntry<TKey, TValue>> content)
        {
            Name = name;
            Content = content;
        }

        public TheDictionary(string name)
        {
            Name=name;
        }
    }
}
