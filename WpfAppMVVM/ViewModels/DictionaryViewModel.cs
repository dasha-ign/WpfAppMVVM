using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfAppMVVM.WPF.Services;
using WpfAppMVVM.WPF.Stores;
using WpfAppMVVM.WPF.ViewModels.Base;

namespace WpfAppMVVM.WPF.ViewModels
{
    internal class DictionaryViewModel : ViewModelBase
    {
        private readonly IDictionaryService _dictionaryService;
        private readonly DictionaryStore _dictionaryStore;


        public ICommand AddNewDictionaryCommand;

        #region view model properties

        public DictionaryListViewModel dictionaryListViewModel { get; }

        public DictionaryContentViewModel dictionaryContentViewModel { get; }

        #endregion

        public DictionaryViewModel(DictionaryStore dictionaryStore)
        {
            _dictionaryStore = dictionaryStore;
            dictionaryListViewModel = new DictionaryListViewModel(_dictionaryStore);
            dictionaryContentViewModel = new DictionaryContentViewModel(_dictionaryStore);
            
        }
    }
}
