using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.S3Object;

namespace TAS.Application.Services.Interfaces
{
    public interface IS3StorageService
    {
        public Task<bool> UploadFileAsync(S3RequestData obj);
        public string GetFileUrl(S3RequestData obj);
        public Task<bool> UploadFolderAsync(S3RquestDirectory obj);
        public Task<Stream> DownloadFileContent(S3RquestDirectory obj);
        public Task<bool> DeleteDirectory(S3RquestDirectory obj);
        public string GetFileUrlDontExpires(S3RequestData obj);
    }
}
