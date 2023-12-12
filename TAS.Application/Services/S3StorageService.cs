using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3;
using Microsoft.Extensions.Logging;
using TAS.Application.Services.Interfaces;
using TAS.Data.S3Object;
using TAS.Infrastructure.Configurations;

namespace TAS.Application.Services
{
    public class S3StorageService : IS3StorageService
    {
        private readonly ILogger<S3StorageService> _logger;

        /// <summary>
        /// Create S3 connection
        /// </summary>
        /// <returns>AmazonS3Client</returns>
        private AmazonS3Client CreateConnection()
        {
            AmazonS3Config config = new AmazonS3Config()
            {
                ServiceURL = AppSettings.ObjectStorageURL,
                ForcePathStyle = true,
            };

            AmazonS3Client client = new AmazonS3Client(AppSettings.ObjectStorageAccessId, AppSettings.ObjectStorageAccessKey, config);
            return client;
        }

        /// <summary>
        /// Upload file to s3
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool</returns>
        public async Task<bool> UploadFileAsync(S3RequestData obj)
        {
            AmazonS3Client client = null;
            try
            {
                client = CreateConnection();
                var transferUtility = new TransferUtility(client);

                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = obj.InputStream,
                    FilePath = obj.filePath,
                    Key = obj.Name,
                    BucketName = obj.BucketName,
                    CannedACL = S3CannedACL.PublicRead,
                };

                await transferUtility.UploadAsync(uploadRequest).ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }
            return false;
        }

        /// <summary>
        /// Generate access url for object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>
        public string GetFileUrl(S3RequestData obj)
        {
            AmazonS3Client client = null;
            try
            {
                client = CreateConnection();

                var fileRequest = new GetPreSignedUrlRequest()
                {
                    Key = obj.Name,
                    BucketName = obj.BucketName,
                    Protocol = Protocol.HTTPS,
                    Expires = DateTime.Now.AddMinutes(10),
                };

                    string url = client.GetPreSignedURL(fileRequest);
                return url;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Upload folder
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool</returns>
        public async Task<bool> UploadFolderAsync(S3RquestDirectory obj)
        {
            AmazonS3Client client = null;
            try
            {
                client = CreateConnection();
                var transferUtility = new TransferUtility(client);

                var uploadRequest = new TransferUtilityUploadDirectoryRequest()
                {
                    BucketName = obj.BucketName,
                    CannedACL = S3CannedACL.PublicRead,
                    KeyPrefix = obj.Prefix,
                    SearchOption = SearchOption.AllDirectories,
                    SearchPattern = "*.*",
                    Directory = obj.Directory
                };

                await transferUtility.UploadDirectoryAsync(uploadRequest)
                    .ConfigureAwait(false);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }
            return false;
        }

        /// <summary>
        /// Downloaf file from bucket
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Stream</returns>
        public async Task<Stream> DownloadFileContent(S3RquestDirectory obj)
        {
            AmazonS3Client client = null;
            try
            {
                client = CreateConnection();
                var transferUtility = new TransferUtility(client);

                var downloadRequest = new TransferUtilityOpenStreamRequest
                {
                    BucketName = obj.BucketName,
                    Key = obj.Prefix,
                };

                var response = await transferUtility.OpenStreamAsync(downloadRequest)
                    .ConfigureAwait(false);
                return response;

            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }

            return Stream.Null;
        }

        /// <summary>
        /// Delete directory in bucket
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteDirectory(S3RquestDirectory obj)
        {
            AmazonS3Client client = null;

            try
            {
                client = CreateConnection();
                var objectResponses = await client.ListObjectsV2Async(new ListObjectsV2Request()
                {
                    BucketName = obj.BucketName,
                    Prefix = obj.Prefix,

                });

                foreach (var objectResponse in objectResponses.S3Objects)
                {
                    var response = await client.DeleteObjectAsync(objectResponse.BucketName, objectResponse.Key)
                    .ConfigureAwait(false);
                    await Console.Out.WriteLineAsync(response.HttpStatusCode.ToString());
                }

                return true;

            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }

            return false;
        }
    }
}
