using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfAppMVVM.WPF.Models;
using WpfAppMVVM.WPF.Stores;
using WpfAppMVVM.WPF.ViewModels.Base;

namespace WpfAppMVVM.WPF.ViewModels
{
    internal class DictionaryListItemViewModel : ViewModelBase
    {
        public TheDictionary<string, string> Dictionary { get; }

        public string Name => Dictionary.Name;

        public ICommand RenameCommand { get; }
        public ICommand DeleteCommand { get; }

        public DictionaryListItemViewModel(TheDictionary<string, string> dictionary)
        {
            Dictionary = dictionary;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
