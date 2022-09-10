using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using NPOI.HPSF;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows;
using WpfAppMVVM.WPF.Models;
using WpfAppMVVM.WPF.Stores;

namespace WellDataLoader.Services
{
    public class DictionaryService
    {
        private readonly HeaderDictionaryStore _headerDictionaryStore;

        public string GetFileName(string dictionaryName, bool variations = false)
        {
            return Path.Combine(variations == true ?
                                _headerDictionaryStore.DictionaryVariationsPath
                                : _headerDictionaryStore.DictionaryPath,           
                                Path.GetFileNameWithoutExtension(dictionaryName) + ".json");
        }

        public async Task<TheObservableDictionary<string, string>> ReadDictionaryContentFromJson(string dictionaryName)
        {
            string fileName = GetFileName(dictionaryName);
            if (!File.Exists(fileName) || new FileInfo(fileName).Length == 0)
                return null;

            using FileStream stream = File.OpenRead(fileName);
            var dictionary = new TheObservableDictionary<string,string>(await System.Text.Json.JsonSerializer.DeserializeAsync<Dictionary<string, string>>(stream));
            await stream.DisposeAsync();
            return dictionary;
        }

 
        public async Task<Dictionary<string, string>> GuessColumnDictionaryValues(List<string> columnValues, string dictionaryName)
        {
            Dictionary<string, string> resultDictionary = new();
            foreach (var columnValue in columnValues)
            {
                resultDictionary.Add(columnValue, String.Empty);
            }
            //загрузить словарь правильных значение
            //загрузить словарь вариаций значений
            string etaloninDictionary = GetFileName(dictionaryName);

            Dictionary<string, string> etalon = await LoadDictionary<string,string>(etaloninDictionary);

            Dictionary<string, List<string>> variations = await LoadDictionary<string,List<string>>(dictionaryName,variations:true);
            if (variations == null)
                return resultDictionary;

            foreach (string columnValue in columnValues)
            {
                //   resultDictionary.Add(columnValue,String.Empty);
                foreach (var pair in variations)
                {
                    if (pair.Value.Contains(columnValue))
                    {
                        resultDictionary[columnValue] = etalon[pair.Key];
                    }
                }
            }
            return resultDictionary;
        }


        public async Task<Dictionary<TKey, TValue>> LoadDictionary<TKey, TValue>(string dictionaryName, bool variations = false)
        {
            string jsonFile = GetFileName(dictionaryName, variations);
            if (!File.Exists(jsonFile) || new FileInfo(jsonFile).Length == 0)
                return null;

            try
            {
                using FileStream stream = File.OpenRead(jsonFile);

                Dictionary<TKey, TValue> resultDictionary = await System.Text.Json.JsonSerializer.DeserializeAsync<Dictionary<TKey, TValue>>(stream);
                await stream.DisposeAsync();
                return resultDictionary;
            }
            catch (Exception e)
            {
                MessageBox.Show("Не удалось прочитать данные словаря. Неверный формат словаря");
                return null;
            }
        }

        public async Task<Dictionary<string, List<string>>> CreateVariationsDictionary(string name)
        {
            string jsonFile = GetFileName(name, variations: true);
            Dictionary<string, string> initialDictionary = await LoadDictionary<string, string>(GetFileName(name));
            Dictionary<string, List<string>> resultDictionary = new Dictionary<string, List<string>>();
            foreach (var pair in initialDictionary)
            {
                var list = new List<string>();
                list.Add(pair.Value);
                resultDictionary.Add(pair.Key, list);
            }
            await SaveDictionary<string,List<string>>(name, resultDictionary, variations:true);
            return resultDictionary;
        }



        public async Task<List<string>> LoadDictionaryList()
        {
            if (string.IsNullOrEmpty(_headerDictionaryStore.DictionaryPath))
                return null;
            var result = new List<string>();
            foreach (var fileName in Directory.GetFiles(_headerDictionaryStore.DictionaryPath, "*.json"))
            {
                result.Add(Path.GetFileNameWithoutExtension(fileName));
            }
            return result;
        }



        public async Task SaveDictionary<TKey, TValue>(string name, Dictionary<TKey, TValue> content, bool variations = false)
        {
            if (content == null || content.Count == 0)
                return;

            string fileName = GetFileName(name, variations);
            

            using FileStream stream = File.Create(fileName);
            await System.Text.Json.JsonSerializer.SerializeAsync<IDictionary<TKey, TValue>>(stream,
                content,
                options: new JsonSerializerOptions
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
                });
            await stream.DisposeAsync();
        }


        public async Task SaveDictionary<TKey, TValue>(string name, TheObservableDictionary<TKey, TValue> content, bool variations = false)
            => await SaveDictionary<TKey, TValue>(name, content.ToDictionary(), variations);



        public bool HasKeyDuplications(TheObservableDictionary<string, string> dictionary) => dictionary?.Keys?.Distinct().Count() < dictionary.Count;


        public async Task FillDescriptions(TheObservableDictionary<int, string> dictionary)
        {
            Dictionary<string,string> dictionaryValues = await LoadDictionary<string, string>(_headerDictionaryStore.CurrentColumnHeaderDictionary);
            foreach (var entry in dictionary)
            {
                entry.EntryDescription = dictionaryValues.Count(item => item.Key.ToLower() == entry.EntryValue.ToLower()) > 0 ?
                                        dictionaryValues.FirstOrDefault(item => item.Key.ToLower() == entry.EntryValue.ToLower()).Value
                                        : string.Empty;
            }
        }



         public async Task WriteDictionaryToJson<TKey, TValue>(string dictionaryPath, IDictionary<TKey,TValue> dictionary)
        {
            using FileStream stream = File.Create(dictionaryPath);

            await System.Text.Json.JsonSerializer.SerializeAsync<IDictionary<TKey, TValue>>(stream,
                                            dictionary,
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


        public DictionaryService(HeaderDictionaryStore headerDictionaryStore)
        {
            _headerDictionaryStore = headerDictionaryStore;
        }
    }
}
