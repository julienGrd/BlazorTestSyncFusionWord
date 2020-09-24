using Syncfusion.EJ2.SpellChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTestSyncFusionWord.Server
{
    public class SpellCheckService
    {
        public List<SpellCheckDictionary> SpellDictCollection { get; set; }

        public void Load(string pBasePath)
        {
            string dictionaryPath = pBasePath + "\\App_Data\\fr.dic";
            string affixPath = pBasePath + "\\App_Data\\fr.aff";
            string customDic = pBasePath + "\\App_Data\\custom.dic";
            SpellDictCollection = new List<SpellCheckDictionary>();
            var lDictionnaryData = new DictionaryData(1046, dictionaryPath, affixPath, customDic);
            SpellDictCollection.Add(new SpellCheckDictionary(lDictionnaryData));
        }
    }
}
