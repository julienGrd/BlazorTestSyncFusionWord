using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syncfusion.Blazor.DocumentEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BlazorTestSyncFusionWord.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordController : ControllerBase
    {

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
    }
}
