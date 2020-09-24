using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syncfusion.Blazor.DocumentEditor;
using Syncfusion.EJ2.SpellChecker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BlazorTestSyncFusionWord.Server.Controllers
{
    [Route("api/[controller]")]
    public class WordController : Controller
    {
        private SpellCheckService _spellCheckService;
        public WordController(SpellCheckService spellCheckService)
        {
            _spellCheckService = spellCheckService;
        }

        [HttpGet]
        public byte[] Get()
        {
            string fileUrl = "https://www.syncfusion.com/downloads/support/directtrac/general/doc/Getting_Started1018066633.docx";
            WebClient webClient = new WebClient();
            byte[] byteArray = webClient.DownloadData(fileUrl);
            return byteArray;
            //Stream stream = new MemoryStream(byteArray);
            //WordDocument document = WordDocument.Load(stream, ImportFormatType.Docx);
            //var sfdtString = JsonConvert.SerializeObject(document);
            //document.Dispose();
            //stream.Dispose();
            //return sfdtString;
        }

        [HttpPost("Save")]
        public async Task Save(byte[] pData)
        {
            Stream stream = new MemoryStream(pData);
            string path = @"saved_doc.docx";
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                //Saving the new file in root path of application
                await stream.CopyToAsync(fileStream);
                fileStream.Close();
            }
            stream.Close();
        }

        [AcceptVerbs("Post")]
        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        [Route("SpellCheck")]
        public string SpellCheck([FromBody] SpellCheckJsonData spellChecker)
        {
            try
            {
                SpellChecker spellCheck = new SpellChecker(_spellCheckService.SpellDictCollection);
                spellCheck.GetSuggestions(spellChecker.LanguageID, spellChecker.TexttoCheck, spellChecker.CheckSpelling, spellChecker.CheckSuggestion, spellChecker.AddWord);
                return Newtonsoft.Json.JsonConvert.SerializeObject(spellCheck);
            }
            catch (Exception ex)
            {
                return "{\"SpellCollection\":[],\"HasSpellingError\":false,\"Suggestions\":null}";
            }
        }

        [AcceptVerbs("Post")]
        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        [Route("SpellCheckByPage")]
        public string SpellCheckByPage([FromBody] SpellCheckJsonData spellChecker)
        {
            try
            {
                SpellChecker spellCheck = new SpellChecker(_spellCheckService.SpellDictCollection);
                spellCheck.CheckSpelling(spellChecker.LanguageID, spellChecker.TexttoCheck);
                return Newtonsoft.Json.JsonConvert.SerializeObject(spellCheck);
            }
            catch (Exception ex)
            {
                return "{\"SpellCollection\":[],\"HasSpellingError\":false,\"Suggestions\":null}";
            }
        }
    }

    public class SpellCheckJsonData
    {
        public int LanguageID { get; set; }
        public string TexttoCheck { get; set; }
        public bool CheckSpelling { get; set; }
        public bool CheckSuggestion { get; set; }
        public bool AddWord { get; set; }

    }
}
