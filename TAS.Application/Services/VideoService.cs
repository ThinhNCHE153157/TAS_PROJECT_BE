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

namespace TAS.Application.Services
{
    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<VideoService> _logger;

        public VideoService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<VideoService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> CreateVideo(AddVideoToTopicRequestDto request)
        {
            try
            {
                var video = _mapper.Map<Video>(request);
                if (video != null)
                {
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

        public Task<bool> UpdateVideo(string videoName, string videoPath)
        {
            throw new NotImplementedException();
        }
    }
}
