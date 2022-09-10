using System.Collections.Generic;
using System.Threading.Tasks;
using WpfAppMVVM.WPF.Models;

namespace WellDataLoader.Services
{
    public interface IDictionaryService
    {
        Task<Dictionary<string, List<string>>> CreateVariationsDictionary(string name);
        Task FillDescriptions(TheObservableDictionary<int, string> dictionary);
        string GetFileName(string dictionaryName, bool variations = false);
        Task<Dictionary<string, string>> GuessColumnDictionaryValues(List<string> columnValues, string dictionaryName);
        bool HasKeyDuplications(TheObservableDictionary<string, string> dictionary);
        Task<Dictionary<TKey, TValue>> LoadDictionary<TKey, TValue>(string dictionaryName, bool variations = false);
        Task<List<string>> LoadDictionaryList();
        Task<TheObservableDictionary<string, string>> ReadDictionaryContentFromJson(string dictionaryName);
        Task SaveDictionary<TKey, TValue>(string name, Dictionary<TKey, TValue> content, bool variations = false);
        Task SaveDictionary<TKey, TValue>(string name, TheObservableDictionary<TKey, TValue> content, bool variations = false);
        Task WriteDictionaryToJson<TKey, TValue>(string dictionaryPath, IDictionary<TKey, TValue> dictionary);
    }
}