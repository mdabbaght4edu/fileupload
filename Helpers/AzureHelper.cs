using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadingLagreFiles_JavaScriptFileSplit.Helpers
{
    public static class AzureHelper
    {
        private static IConfiguration _configuration;
        public static IConfiguration Configuration => _configuration ??= new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        //public static IConfiguration Configuration { get; set; }
        //public static void Configure(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public static class Blob
        {
            public class UploadResult
            {
                public string FileRelativePath { get; set; }
                public string FileAbsolutePath { get; set; }
                public BlobContentInfo BlobContentInfo { get; set; }
            }
            public class AzureBlobStorage
            {
                public string AccountName { get; set; }
                public string AccountKey { get; set; }
                public string EndpointSuffix { get; set; }
                public string ContainerName { get; set; }
                public int SASTokenDurationInMinutes { get; set; }
            }

            private static readonly AzureBlobStorage azureBlobStorage = Configuration?.GetSection("AzureBlobStorage")?.Get<AzureBlobStorage>();

            private static readonly string AccountName = azureBlobStorage?.AccountName;
            private static readonly string AccountKey = azureBlobStorage?.AccountKey;
            private static readonly string EndpointSuffix = azureBlobStorage?.EndpointSuffix;
            private static readonly string BlobContainerName = azureBlobStorage?.ContainerName;
            private static readonly int SASTokenDurationInMinutes = azureBlobStorage?.SASTokenDurationInMinutes??60;

            private static readonly string DefaultEndpointsProtocol = "https";
            private static readonly string ConnectionString = $"DefaultEndpointsProtocol={DefaultEndpointsProtocol};AccountName={AccountName};AccountKey={AccountKey};EndpointSuffix={EndpointSuffix}";
            private static readonly string BlobHostPath = $"{DefaultEndpointsProtocol}://{AccountName}.{EndpointSuffix}";
            private static readonly StorageSharedKeyCredential SharedKey = Configuration==null? null: new StorageSharedKeyCredential(AccountName, AccountKey);
            private static readonly string BlobContainerPath = BlobHostPath + "/" + BlobContainerName;

            private static BlobServiceClient _blobServiceClient;
            private static BlobServiceClient BlobServiceClient => _blobServiceClient ?? (_blobServiceClient = new BlobServiceClient(ConnectionString));

            private static BlobContainerClient _blobContainerClient;
            private static BlobContainerClient VSchoolBlobContainerClient => _blobContainerClient ?? (_blobContainerClient = BlobServiceClient.GetBlobContainerClient(BlobContainerName));

            public static UploadResult Upload(IFormFile file, string remoteFilePath, bool isTempFile = false)
            {
                return Upload(file.OpenReadStream(), remoteFilePath, isTempFile);
            }

            public static UploadResult Upload(string localFilePath, string remoteFilePath, bool isTempFile = false)
            {
                if (string.IsNullOrWhiteSpace(localFilePath) || !File.Exists(localFilePath) || string.IsNullOrWhiteSpace(remoteFilePath))
                {
                    return null;
                }

                try
                {
                    using (var file = File.OpenRead(localFilePath))
                    {
                        return Upload(file, remoteFilePath, isTempFile);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            public static UploadResult Upload(Stream fileStream, string remoteFilePath, bool isTempFile = false)
            {
                if (fileStream == null || fileStream.Length == 0 || string.IsNullOrWhiteSpace(remoteFilePath))
                {
                    return null;
                }

                try
                {
                    remoteFilePath = remoteFilePath.Trim(new char[] { ' ', '/' });
                    var response = VSchoolBlobContainerClient.UploadBlob(remoteFilePath, fileStream);
                    var azureFilePath = "/" + BlobContainerName + "/" + remoteFilePath;
                    if (isTempFile)
                    {
                        var blobClient = VSchoolBlobContainerClient.GetBlobClient(remoteFilePath);
                        var data = new Dictionary<string, string>
                        {
                            ["isTempFile"] = isTempFile.ToString()
                        };
                        // Exception: value for one of the query parameters specified in the request uri is invalid
                        //Task.Run(() => 
                        //{
                        //    var tagsResponse = blobClient.GetTags();
                        //    var tags = tagsResponse.Value.Tags;
                        //    blobClient.SetTags(tags, new BlobRequestConditions { IfMatch = response.Value.ETag });
                        //});
                        //blobClient.SetTags(data, new BlobRequestConditions { IfMatch = response.Value.ETag });
                        blobClient.SetMetadata(data);
                    }
                    return new UploadResult()
                    {
                        FileRelativePath = azureFilePath,
                        FileAbsolutePath = BlobHostPath + azureFilePath,
                        BlobContentInfo = response.Value,
                    };
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            public static bool Copy(string srcFilePath, string destFilePath, bool deleteSourceFile = false)
            {
                if (string.IsNullOrWhiteSpace(srcFilePath) || string.IsNullOrWhiteSpace(destFilePath))
                {
                    return false;
                }

                try
                {
                    srcFilePath = srcFilePath.Trim(new char[] { ' ', '/' });
                    if (srcFilePath.StartsWith(BlobContainerName, StringComparison.OrdinalIgnoreCase))
                    {
                        srcFilePath = srcFilePath.Replace(BlobContainerName, "").Trim(new char[] { ' ', '/' });
                    }
                    destFilePath = destFilePath.Trim(new char[] { ' ', '/' });
                    if (destFilePath.StartsWith(BlobContainerName, StringComparison.OrdinalIgnoreCase))
                    {
                        destFilePath = destFilePath.Replace(BlobContainerName, "").Trim(new char[] { ' ', '/' });
                    }

                    // Create a BlobClient representing the source blob to copy.
                    BlobClient sourceBlob = VSchoolBlobContainerClient.GetBlobClient(srcFilePath);

                    // Ensure that the source blob exists.
                    if (!sourceBlob.Exists())
                    {
                        return false;
                    }

                    // Lease the source blob for the copy operation 
                    // to prevent another client from modifying it.
                    BlobLeaseClient lease = sourceBlob.GetBlobLeaseClient();

                    // Specifying -1 for the lease interval creates an infinite lease.
                    lease.Acquire(TimeSpan.FromSeconds(60D));

                    // Get the source blob's properties and display the lease state.
                    BlobProperties sourceProperties = sourceBlob.GetProperties();

                    // Get a BlobClient representing the destination blob with a unique name.
                    BlobClient destBlob = VSchoolBlobContainerClient.GetBlobClient(destFilePath);

                    // Start the copy operation.
                    var copyResult = destBlob.StartCopyFromUri(sourceBlob.Uri);

                    // Update the source blob's properties.
                    sourceProperties = sourceBlob.GetProperties();

                    if (sourceProperties.LeaseState == LeaseState.Leased)
                    {
                        // Break the lease on the source blob.
                        lease.Break();
                    }

                    if (deleteSourceFile)
                    {
                        // Get the destination blob's properties and display the copy status.
                        BlobProperties destProperties = destBlob.GetProperties();

                        if (destBlob.Exists())
                        {
                            sourceBlob.DeleteIfExists();
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            public static Uri GetDownloadLink(string relativePath)
            {
                if (string.IsNullOrWhiteSpace(relativePath) || azureBlobStorage == null)
                {
                    return null;
                }

                relativePath = relativePath.Trim(new char[] { ' ', '/' });
                var blobName = relativePath.Replace(BlobContainerName + "/", "");
                var url = GetBlobSasUri(blobName);
                return url; // BlobContainerPath + relativePath;
            }

            public static Stream Download(string filePath)
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    return null;
                }

                try
                {
                    filePath = filePath.Trim(new char[] { ' ', '/' });
                    if (filePath.StartsWith(BlobContainerName, StringComparison.OrdinalIgnoreCase))
                    {
                        filePath = filePath.Replace(BlobContainerName, "").Trim(new char[] { ' ', '/' });
                    }

                    var blobClient = VSchoolBlobContainerClient.GetBlobClient(filePath);
                    var stream = new MemoryStream();
                    blobClient.DownloadTo(stream);
                    return stream;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            public static bool IsBlobPath(string path)
            {
                if (string.IsNullOrWhiteSpace(path)) return false;

                path = path.Trim(new char[] { ' ', '/' });
                return path.StartsWith(BlobContainerName + "/", StringComparison.OrdinalIgnoreCase);
            }

            private static Uri GetBlobSasUri(string blobName, string storedPolicyName = null, BlobSasPermissions? permissions = null)
            {
                // Create a SAS token that's valid for one hour.
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = BlobContainerName,
                    BlobName = blobName,
                    Resource = "b",
                };

				if (!string.IsNullOrWhiteSpace(storedPolicyName))
				{
                    sasBuilder.Identifier = storedPolicyName;
				}
				else if (permissions.HasValue)
				{
                    sasBuilder.StartsOn = DateTimeOffset.UtcNow;
                    sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(SASTokenDurationInMinutes);
                    sasBuilder.SetPermissions(permissions.Value);
                }
				else
				{
                    sasBuilder.StartsOn = DateTimeOffset.UtcNow;
                    sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(SASTokenDurationInMinutes);
                    sasBuilder.SetPermissions(BlobSasPermissions.Read);
                }

                // Use the key to get the SAS token.
                string sasToken = sasBuilder.ToSasQueryParameters(SharedKey).ToString();

                BlobClient blobClient = VSchoolBlobContainerClient.GetBlobClient(blobName);

                return new Uri($"{blobClient.Uri}?{sasToken}");
            }

            public static Uri GetUploadLink(string relativePath)
            {
                if (string.IsNullOrWhiteSpace(relativePath) || azureBlobStorage == null)
                {
                    return null;
                }

                relativePath = relativePath.Trim(new char[] { ' ', '/' });
                var blobName = relativePath.Replace(BlobContainerName + "/", "");
                var url = GetBlobSasUri(blobName, permissions: BlobSasPermissions.Write);
                return url; // BlobContainerPath + relativePath;
            }
        }
    }

    public static partial class HttpPostedFileExtensions
    {
        public static AzureHelper.Blob.UploadResult UploadToAzureBlob(this IFormFile httpPostedFile, string remoteFilePath, bool isTempFile = false)
        {
            return AzureHelper.Blob.Upload(httpPostedFile, remoteFilePath, isTempFile);
        }
    }
}
