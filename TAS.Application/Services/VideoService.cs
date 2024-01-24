using Amazon.Runtime.Internal.Util;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.EF;
using TAS.Data.Entities;
using TAS.Data.S3Object;
using static System.Net.Mime.MediaTypeNames;

namespace TAS.Application.Services
{
    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IS3StorageService _s3StorageService;
        private readonly ITopicService _topicService;
        private readonly ILogger<VideoService> _logger;

        public VideoService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<VideoService> logger, IS3StorageService s3StorageService, ITopicService topicService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _s3StorageService = s3StorageService;
            _topicService = topicService;
        }

        public async Task<bool> AddVideoToTopic(List<AddVideoToTopicRequestDto> request)
        {
            try
            {
                foreach (var item in request)
                {
                    Video video = new Video();
                    if (item != null)
                    {
                        S3RequestData s3RequestData = new S3RequestData
                        {
                            BucketName = "tas",
                            InputStream = item.VideoUrl.OpenReadStream(),
                            Name = item.VideoUrl.FileName,
                        };
                        await _s3StorageService.UploadFileAsync(s3RequestData).ConfigureAwait(false);
                        video.VideoUrl = _s3StorageService.GetFileUrlDontExpires(s3RequestData);
                        if (item.VideoAttachment != null)
                        {
                            S3RequestData s3RequestData1 = new S3RequestData
                            {
                                BucketName = "tas",
                                InputStream = item.VideoAttachment.OpenReadStream(),
                                Name = item.VideoAttachment.FileName,
                            };
                            await _s3StorageService.UploadFileAsync(s3RequestData1).ConfigureAwait(false);
                            video.VideoAttachment = _s3StorageService.GetFileUrlDontExpires(s3RequestData1);
                        }
                        var topic = await _topicService.GetTopicByName(item.TopicName);
                        if (topic!=null)
                        {
                            video.TopicId = topic.TopicId;
                        }
                        video.VideoDescription = item.VideoDescription;
                        video.VideoTitle = item.VideoTitle;
                        await _unitOfWork.VideoRepository.AddAsync(video).ConfigureAwait(false);
                        await _unitOfWork.CommitAsync().ConfigureAwait(false);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }


        public async Task<bool> CreateVideo(AddVideoToTopicRequestDto request)
        {
            try
            {
                var video = _mapper.Map<Video>(request);
                if (video != null)
                {
                    video.VideoUrl = "";
                    await _unitOfWork.VideoRepository.AddAsync(video).ConfigureAwait(false);
                    await _unitOfWork.CommitAsync().ConfigureAwait(false);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }

        }

        public Task<bool> DeleteVideo(string videoName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetVideo(string videoName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateVideo(UpdateVideoRequestDto request)
        {
            string videoUrl = "";
            try
            {
                if (request != null)
                {
                    if (request.Video != null)
                    {
                        S3RequestData s3RequestData = new S3RequestData
                        {
                            BucketName = "tas",
                            InputStream = request.Video.OpenReadStream(),
                            Name = request.Video.FileName,
                        };
                        await _s3StorageService.UploadFileAsync(s3RequestData).ConfigureAwait(false);
                        videoUrl = _s3StorageService.GetFileUrlDontExpires(s3RequestData);
                    }

                    var videoToUpdate = _unitOfWork.VideoRepository.GetById(request.VideoId);
                    if (videoToUpdate != null)
                    {
                        videoToUpdate.VideoUrl = videoUrl;
                        _unitOfWork.VideoRepository.updateVideo(videoToUpdate);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
}
