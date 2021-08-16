﻿using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
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
using UploadingLagreFiles_JavaScriptFileSplit.Helpers;
using UploadingLagreFiles_JavaScriptFileSplit.Models;

namespace UploadingLagreFiles_JavaScriptFileSplit.Controllers
{
	public class AzureStorageSASResult
	{
		public string AccountName { get; set; }
		public string AccountUrl { get; set; }
		public Uri ContainerUri { get; set; }
		public string ContainerName { get; set; }
		public string BlobName { get; set; }
		public Uri SASUri { get; set; }
		public string SASToken { get; set; }
		public string SASPermission { get; set; }
		public DateTimeOffset SASExpire { get; set; }
	}

	public class AzureController : Controller
	{
		private readonly ILogger<AzureController> _logger;
		private IHostingEnvironment Environment;

		public AzureController(ILogger<AzureController> logger, IHostingEnvironment _environment)
		{
			_logger = logger;
			Environment = _environment;
		}

		//[HttpGet]
		//public IActionResult Index1()
		//{
		//	var AccountName = "vschooldev";
		//	var AccountKey = "wfm1Y3R6Oy3Ov04as8XCD7AM9R6XR0t5hrvpCg3jgnEMgw4jc+bCJubSXu5Fa3rCEpBhD8+9UjqsBksy3VR8xA==";
		//	var ConnectionString = "DefaultEndpointsProtocol=https;AccountName=vschooldev;AccountKey=wfm1Y3R6Oy3Ov04as8XCD7AM9R6XR0t5hrvpCg3jgnEMgw4jc+bCJubSXu5Fa3rCEpBhD8+9UjqsBksy3VR8xA==;EndpointSuffix=core.windows.net;";
		//	var ContainerName = "lor";
		//	var TokenExpirationMinutes = 60;
		//	var blobName = "test.zip";

		//	BlobContainerClient container = new(ConnectionString, ContainerName);

		//	if (!container.CanGenerateSasUri) return Conflict("The container can't generate SAS URI");

		//	var sasBuilder = new BlobSasBuilder
		//	{
		//		BlobContainerName = container.Name,
		//		Resource = "c",
		//		ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(TokenExpirationMinutes)
		//	};

		//	sasBuilder.SetPermissions(BlobAccountSasPermissions.All);
		//	sasBuilder.SetPermissions(BlobContainerSasPermissions.All);
		//	sasBuilder.SetPermissions(BlobSasPermissions.All);

		//	var sasUri = container.GenerateSasUri(sasBuilder);

		//	var result = new AzureStorageSASResult
		//	{
		//		AccountName = container.AccountName,
		//		AccountUrl = $"{container.Uri.Scheme}://{container.Uri.Host}",
		//		ContainerName = container.Name,
		//		ContainerUri = container.Uri,
		//		BlobName = blobName,
		//		SASUri = sasUri,
		//		SASToken = sasUri.Query.TrimStart('?'),
		//		SASPermission = sasBuilder.Permissions,
		//		SASExpire = sasBuilder.ExpiresOn
		//	};

		//	return View(result);
		//}

		//[HttpGet]
		//public IActionResult Index2()
		//{
		//	var AccountName = "vschooldev";
		//	var AccountKey = "wfm1Y3R6Oy3Ov04as8XCD7AM9R6XR0t5hrvpCg3jgnEMgw4jc+bCJubSXu5Fa3rCEpBhD8+9UjqsBksy3VR8xA==";
		//	//var ConnectionString = "DefaultEndpointsProtocol=https;AccountName=vschooldev;AccountKey=wfm1Y3R6Oy3Ov04as8XCD7AM9R6XR0t5hrvpCg3jgnEMgw4jc+bCJubSXu5Fa3rCEpBhD8+9UjqsBksy3VR8xA==;EndpointSuffix=core.windows.net;";
		//	var ContainerName = "lor";
		//	var TokenExpirationMinutes = 60 * 24;
		//	var blobName = "test.zip";
		//	var SharedKey = new StorageSharedKeyCredential(AccountName, AccountKey);

		//	var blobServiceClient = new BlobServiceClient(new Uri("https://vschooldev.blob.core.windows.net"), SharedKey);

		//	var blobContainerClient = new BlobContainerClient(blobServiceClient.GetBlobContainerClient(ContainerName).Uri, SharedKey);

		//	var blobClient = new BlobClient(blobContainerClient.GetBlobClient(blobName).Uri, SharedKey);
		//	if (!blobClient.CanGenerateSasUri)
		//	{
		//		return Conflict("The blob can't generate SAS URI");
		//	}

		//	// Create a SAS token that's valid for one hour.
		//	var sasBuilder = new BlobSasBuilder()
		//	{
		//		BlobContainerName = ContainerName,
		//		BlobName = blobName,
		//		Resource = "b",
		//	};

		//	sasBuilder.StartsOn = DateTimeOffset.UtcNow;
		//	sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(TokenExpirationMinutes);
		//	sasBuilder.SetPermissions(BlobSasPermissions.Write);

		//	//var sasUri = blobClient.GenerateSasUri(sasBuilder);

		//	var sasToken = sasBuilder.ToSasQueryParameters(SharedKey).ToString();
		//	var sasUri = new Uri($"{blobClient.Uri}?{sasToken}");

		//	var result = new AzureStorageSASResult
		//	{
		//		AccountName = blobServiceClient.AccountName,
		//		AccountUrl = $"{blobContainerClient.Uri.Scheme}://{blobContainerClient.Uri.Host}",
		//		ContainerName = blobContainerClient.Name,
		//		ContainerUri = blobContainerClient.Uri,
		//		BlobName = blobClient.Name,
		//		SASUri = sasUri,
		//		SASToken = sasUri.Query.TrimStart('?'),
		//		SASPermission = sasBuilder.Permissions,
		//		SASExpire = sasBuilder.ExpiresOn
		//	};

		//	return View(result);
		//}

		[HttpGet]
		public IActionResult Index()
		{
			var blobName = "test.zip";
			var uri = AzureHelper.Blob.GetUploadLink(blobName);

			var result = new AzureStorageSASResult
			{
				AccountUrl = $"{uri.Scheme}://{uri.Host}",
				ContainerName = uri.LocalPath.Replace(blobName, "").Trim('/'),
				BlobName = blobName,
				SASToken = uri.Query.TrimStart('?')
			};

			return View(result);
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
