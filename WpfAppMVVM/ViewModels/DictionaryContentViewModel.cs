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
    internal class DictionaryContentViewModel : ViewModelBase
    {
        private readonly DictionaryStore _dictionaryStore;

        public bool HasSelectedDictionary => _dictionaryStore.SelectedDictionary != null;

        #region DictionaryEntries : ObservableCollection<DictionaryEntry<string,string>> - list of dictionary contents

        ///<summary>list of dictionary contents</summary>
        public ObservableCollection<DictionaryEntry<string, string>>? DictionaryEntries
        {
            get => _dictionaryStore.SelectedDictionary?.Content ;
        }

        #endregion



        public DictionaryContentViewModel(DictionaryStore dictionaryStore)
        {
            _dictionaryStore = dictionaryStore;
            _dictionaryStore.SelectedDictionaryChanged += SelectedDictionaryChanged;
        }

        private void SelectedDictionaryChanged()
        {
            OnPropertyChanged(nameof(HasSelectedDictionary));
            OnPropertyChanged(nameof(DictionaryEntries));
        }


        protected override void Dispose()
        {
            _dictionaryStore.SelectedDictionaryChanged -= SelectedDictionaryChanged;
            base.Dispose();
        }

    }
}
