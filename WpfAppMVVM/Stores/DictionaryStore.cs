using System.Collections.Generic;
using System;
using System.IO;
using WpfAppMVVM.WPF.Models;

namespace WpfAppMVVM.WPF.Stores
{
    public class DictionaryStore
    {
                

        #region DictionaryPath : string? - path for a dictionary to store or read from

        ///<summary>path for etalon dictionary to store or read from</summary>
        private string? _DictionaryPath;

        ///<summary>path for a dictionary to store or read from</summary>
        public string? DictionaryPath
        {
            get => _DictionaryPath;
            set
            {
                _DictionaryPath = value;
                DictionaryPathChanged?.Invoke();
            }
        }

        public event Action DictionaryPathChanged;

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

        public event Action DictionaryVariationsPathChanged;

        #endregion


        #region DictionaryList : List<TheDictionary<string,string>> - list of existing header dictionaries

        ///<summary>list of existing header dictionaries</summary>
        private List<TheDictionary<string, string>>? _DictionaryList;

        ///<summary>list of existing header dictionaries</summary>
        public List<TheDictionary<string, string>>? DictionaryList
        {
            get => _DictionaryList;
            set
            {
                _DictionaryList = value;
                DictionaryListChanged?.Invoke();
            }
        }

        public event Action? DictionaryListChanged;


        #endregion


        #region SelectedDictionary : TheDictionary<string,string> - name of selected dictionary

        ///<summary>name of selected dictionary</summary>
        private TheDictionary<string,string> _SelectedDictionary;

        ///<summary>name of selected dictionary</summary>
        public TheDictionary<string,string> SelectedDictionary
        {
            get => _SelectedDictionary;
            set
            {
                _SelectedDictionary = value;
                SelectedDictionaryChanged?.Invoke();
            }
        }

        public event Action? SelectedDictionaryChanged;

        #endregion
    }
}