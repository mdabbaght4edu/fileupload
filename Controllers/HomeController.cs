using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UploadingLagreFiles_JavaScriptFileSplit.Models;

namespace UploadingLagreFiles_JavaScriptFileSplit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHostingEnvironment Environment;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment _environment)
        {
            _logger = logger;
            Environment = _environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

      
        private async Task<byte[]> GetByteArrayFromImageAsync(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                await file.CopyToAsync(target);
                return target.ToArray();
            }
        }


        [HttpPost]
        public IActionResult UploadFile(IFormFile files)
        {
          
            var FileDataContent = Request.Form.Files[0];
            if (FileDataContent != null && FileDataContent.Length > 0)
            {
                // take the input stream, and save it to a temp folder using  
                // the original file.part name posted  
                var stream = FileDataContent.OpenReadStream();
                var fileName = Path.GetFileName(FileDataContent.FileName);
                var tempFolderName = "yourFolderThatContainSpiltFiles";
                var UploadPath = this.Environment.WebRootPath + "/temp/files/" + tempFolderName;
             
                System.IO.Directory.CreateDirectory(UploadPath);
                string path = Path.Combine(UploadPath, fileName);
                try
                {
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    using (var fileStream = System.IO.File.Create(path))
                    {
                        stream.CopyTo(fileStream);
                    }
                    // Once the file part is saved, see if we have enough to merge it  
                    //UploadingLagreFiles_JavaScriptFileSplit.Models.Shared.Utils UT = new UploadingLagreFiles_JavaScriptFileSplit.Models.Shared.Utils();
                    //UT.MergeFile(path);
                }
                catch (IOException ex)
                {
                    // handle  
                }
                return new JsonResult(new { scusses = true, fileName = FileDataContent.FileName, folderPath = UploadPath });

            }
            return new JsonResult(new { scusses = false });


        }


        [HttpPost]
        public IActionResult MergeFiles(List<string> files)
        {
            CombineMultipleFilesIntoSingleFile(this.Environment.WebRootPath + "/temp/files/yourFolderThatContainSpiltFiles",
                this.Environment.WebRootPath + "/temp/files/yourFolderThatContainSpiltFiles/MergedFile.zip");
            return new JsonResult(new { scusses = true });
        }

        private static void CombineMultipleFilesIntoSingleFile(string inputDirectoryPath, string outputFilePath)
        {
            string[] inputFilePaths = System.IO.Directory.GetFiles(inputDirectoryPath);
            Console.WriteLine("Number of files: {0}.", inputFilePaths.Length);
            using (var outputStream =System.IO.File.Create(outputFilePath))
            {
                foreach (var inputFilePath in inputFilePaths)
                {
                    using (var inputStream = System.IO.File.OpenRead(inputFilePath))
                    {
                        // Buffer size can be passed as the second argument.
                        inputStream.CopyTo(outputStream);
                    }
                    Console.WriteLine("The file {0} has been processed.", inputFilePath);
                }
            }
        }


    }
}
