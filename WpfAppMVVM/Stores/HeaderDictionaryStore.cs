using System.Collections.Generic;
using System;
using System.IO;

namespace WpfAppMVVM.WPF.Stores
{
    public class HeaderDictionaryStore
    {
        public event Action DictionaryPathChanged;
        public event Action DictionaryVariationsPathChanged;
        public event Action DictionaryListChanged;
        public event Action CurrentColumnHeaderDictionaryChanged;

        #region DictionaryPath : string - path for a dictionary to store or read from

        ///<summary>path for etalon dictionary to store or read from</summary>
        private string _DictionaryPath;

        ///<summary>path for a dictionary to store or read from</summary>
        public string DictionaryPath
        {
            get => _DictionaryPath;
            set
            {
                _DictionaryPath = value;
                DictionaryPathChanged?.Invoke();
            }
        }

        #endregion


        #region DictionaryVariationsPath : string - path for variety of values loaded from files

        ///<summary>path for variety of values loaded from files</summary>
        private string _DictionaryVariationsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WellDataLoader\\DictionaryVariations");

        ///<summary>path for variety of values loaded from files</summary>
        public string DictionaryVariationsPath
        {
            get => _DictionaryVariationsPath;
            set
            {
                if (_DictionaryVariationsPath != value) ;
                DictionaryVariationsPathChanged?.Invoke();
            }
        }

        #endregion


        #region DictionaryList : List<string> - list of existing header dictionaries

        ///<summary>list of existing header dictionaries</summary>
        private List<string> _DictionaryList;

        ///<summary>list of existing header dictionaries</summary>
        public List<string> DictionaryList
        {
            get => _DictionaryList;
            set
            {
                _DictionaryList = value;
                DictionaryListChanged?.Invoke();
            }
        }

        #endregion


        #region CurrentColumnHeaderDictionary : string - name of selected column dictionary

        ///<summary>name of selected column dictionary</summary>
        private string _CurrentColumnHeaderDictionary;

        ///<summary>name of selected column dictionary</summary>
        public string CurrentColumnHeaderDictionary
        {
            get => _CurrentColumnHeaderDictionary;
            set
            {
                _CurrentColumnHeaderDictionary = value;
                CurrentColumnHeaderDictionaryChanged?.Invoke();
            }
        }

        #endregion
    }
}