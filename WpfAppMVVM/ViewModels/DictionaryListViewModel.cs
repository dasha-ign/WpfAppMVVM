using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppMVVM.WPF.Models;
using WpfAppMVVM.WPF.Stores;
using WpfAppMVVM.WPF.ViewModels.Base;

namespace WpfAppMVVM.WPF.ViewModels
{
    internal class DictionaryListViewModel : ViewModelBase 
    {
        private readonly DictionaryStore _dictionaryStore;

        private readonly ObservableCollection<DictionaryListItemViewModel> _Dictionaries;
        public IEnumerable<DictionaryListItemViewModel> Dictionaries => _Dictionaries;



        #region SelectedDictionaryViewModel : DictionaryListItemViewModel - selectedDictionary

        ///<summary>selectedDictionary</summary>
        private DictionaryListItemViewModel _SelectedDictionaryViewModel;

        ///<summary>selectedDictionary</summary>
        public DictionaryListItemViewModel SelectedDictionaryViewModel
        {
            get => _SelectedDictionaryViewModel;
            set
            {
                _SelectedDictionaryViewModel = value;
                OnPropertyChanged(nameof(SelectedDictionaryViewModel));

                _dictionaryStore.SelectedDictionary = SelectedDictionaryViewModel.Dictionary;
            }
        }

        #endregion



        public DictionaryListViewModel(DictionaryStore dictionaryStore)
        {
            _dictionaryStore = dictionaryStore;

            _Dictionaries = new();

            for (int i = 0; i < 3; i++)
            {
                string name = $"dictionary_{i}";
                var DictionaryEntries = new ObservableCollection<DictionaryEntry<string,string>>();
                for (int j = 0; j < 10; j++)
                {
                    DictionaryEntries.Add(new($"{name}_Entry_{j}", $"EntryValue_{j}"));
                }
                _Dictionaries.Add(new(new TheDictionary<string, string>(name,DictionaryEntries)));
            }
            
        }
    }
}
