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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UploadingLagreFiles_JavaScriptFileSplit.Models;

namespace UploadingLagreFiles_JavaScriptFileSplit.Controllers
{
	public class UploadViewRequest
	{
		public Guid UploadFolder { get; set; }
		public bool IsChunk { get; set; }
		public int ChunkNumber { get; set; }
		public bool IsFirst { get; set; }
		public bool IsLast { get; set; }
		public IFormFile OriginalFile { get; set; }
		public bool JsonAccepted { get; set; }
	}

	public class FileStatus
	{
		public const string HandlerPath = "/";

		public string group { get; set; }
		public string name { get; set; }
		public string type { get; set; }
		public int size { get; set; }
		public string progress { get; set; }
		public string url { get; set; }
		public string thumbnail_url { get; set; }
		public string delete_url { get; set; }
		public string delete_type { get; set; }
		public string error { get; set; }

		public FileStatus()
		{
		}

		public FileStatus(FileInfo fileInfo)
		{
			SetValues(fileInfo.Name, (int)fileInfo.Length, fileInfo.FullName);
		}

		public FileStatus(string fileName, int fileLength, string fullPath)
		{
			SetValues(fileName, fileLength, fullPath);
		}

		private void SetValues(string fileName, int fileLength, string fullPath)
		{
			type = null;
			name = fileName;
			url = null;
			size = fileLength;
			progress = "1.0";
			delete_url = null;
			delete_type = "DELETE";
			thumbnail_url = null;
		}
	}

	public class UploadController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private IHostingEnvironment Environment;

		public UploadController(ILogger<HomeController> logger, IHostingEnvironment _environment)
		{
			_logger = logger;
			Environment = _environment;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(UploadViewRequest model)
		{
			FileStatus status = null;
			try
			{
				model ??= new UploadViewRequest();

				model.OriginalFile = Request.Form.Files.FirstOrDefault();

				var rangeHeader = Request.Headers["Content-Range"];
				if (string.IsNullOrEmpty(rangeHeader))
				{
					model.IsChunk = false;
				}
				else
				{
					model.IsChunk = true;

					Match match = Regex.Match(rangeHeader, "^bytes ([\\d]+)-([\\d]+)\\/([\\d]+)$", RegexOptions.IgnoreCase);
					int bytesFrom = int.Parse(match.Groups[1].Value);
					int bytesTo = int.Parse(match.Groups[2].Value);
					int bytesFull = int.Parse(match.Groups[3].Value);

					if (bytesTo == bytesFull)
						model.IsLast = true;
					else
						model.IsLast = false;

					if (bytesFrom == 0)
					{
						model.ChunkNumber = 1;
						model.IsFirst = true;
					}
					else
					{
						int bytesSize = bytesTo - bytesFrom + 1;
						model.ChunkNumber = (bytesFrom / bytesSize) + 1;
						model.IsFirst = false;
					}
				}

				if (Request.Headers.ContainsKey("Accept") && Request.Headers["Accept"].ToString().Contains("application/json"))
				{
					model.JsonAccepted = true;
				}
				else
				{
					model.JsonAccepted = false;
				}

				// Section 2
				var fileName = model.OriginalFile.FileName;
				var path = this.Environment.WebRootPath + "/storage/temp/" + fileName;
				
				if (model.IsChunk)
				{
					if (model.IsFirst)
					{
						// do some stuff that has to be done before the file starts uploading
						if (System.IO.File.Exists(path))
						{
							System.IO.File.Delete(path);
						}
					}

					var inputStream = model.OriginalFile.OpenReadStream();

					using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write))
					{
						var buffer = new byte[1024];

						var l = inputStream.Read(buffer, 0, 1024);
						while (l > 0)
						{
							fs.Write(buffer, 0, l);
							l = inputStream.Read(buffer, 0, 1024);
						}

						fs.Flush();
						fs.Close();
					}

					status = new FileStatus(new FileInfo(path));

					if (model.IsLast)
					{
						// do some stuff that has to be done after the file is uploaded
					}
				}
				else
				{
					if (System.IO.File.Exists(path))
					{
						System.IO.File.Delete(path);
					}
					using (Stream fileStream = new FileStream(path, FileMode.Create))
					{
						model.OriginalFile.CopyTo(fileStream);
					}
					status = new FileStatus(new FileInfo(path));
				}
			}
			catch (Exception ex)
			{
				status = new FileStatus
				{
					error = "Something went wrong"
				};
			}

			// this is just a browser json support/compatibility workaround
			if (model.JsonAccepted)
			{
				return Json(status);
			}
			else
			{
				return Json(status, "text/plain");
			}
		}

		[HttpHead]
		public IActionResult Index(string filename)
		{
			var path = this.Environment.WebRootPath + "/storage/temp/" + filename;
			if (System.IO.File.Exists(path))
			{
				return Json(new { status = true, fileSize = new FileInfo(path).Length });
			}
			else
			{
				return Json(new { status = false });
			}
		}
	}
}
