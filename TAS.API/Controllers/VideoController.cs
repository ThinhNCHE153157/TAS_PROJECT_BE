using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAS.Application.Services;
using TAS.Application.Services.Interfaces;
using static TAS.Infrastructure.Constants.ApiRouter;
using TAS.Data.S3Object;
using TAS.Infrastructure.Configurations;
using TAS.Data.Dtos.Requests;

namespace TAS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IS3StorageService _s3StorageService;
        private readonly IVideoService _videoService;
        public VideoController(IS3StorageService s3StorageService, IVideoService videoService)
        {
            _s3StorageService = s3StorageService;
            _videoService = videoService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadVideo(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _s3StorageService.UploadFileAsync(new TAS.Data.S3Object.S3RequestData()
                {
                    BucketName = "tas",
                    InputStream = file.OpenReadStream(),
                    Name = file.FileName,
                });
                if (result)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetVideoUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest();
            }
            try
            {
                var result = _s3StorageService.GetFileUrl(new TAS.Data.S3Object.S3RequestData()
                {
                    BucketName = "tas",
                    Name = fileName,
                });
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> PreviewImage( string fileName)
        {
            var fileStream = await _s3StorageService.DownloadFileContent(new S3RquestDirectory
            {
                BucketName = AppSettings.ObjectStorageBucket,
                Prefix = fileName
            });

            if (fileStream == Stream.Null)
            {
                return NotFound("Segment not found!");
            }

            return File(fileStream, "image/png");
        }        
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> PreviewVideo( string fileName)
        {
            var fileStream = await _s3StorageService.DownloadFileContent(new S3RquestDirectory
            {
                BucketName = AppSettings.ObjectStorageBucket,
                Prefix = fileName
            });

            if (fileStream == Stream.Null)
            {
                return NotFound("Segment not found!");
            }

            return File(fileStream, "video/mp4");
        }

        [HttpPost]
        public async Task<IActionResult> AddVideo(AddVideoToTopicRequestDto request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _videoService.CreateVideo(request);
                if (result)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
