using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfAppMVVM.WPF.ViewModels
{
    internal class DictionaryViewModel : ViewModelBase
    {
        

        public ICommand AddNewDictionaryCommand;

        #region view model properties

        public DictionaryListViewModel dictionaryListViewModel { get; }

        public DictionaryContentViewModel dictionaryContentViewModel { get; }

        #endregion

        public DictionaryViewModel()
        {
            dictionaryListViewModel = new DictionaryListViewModel();
            dictionaryContentViewModel = new DictionaryContentViewModel();
        }
    }
}
