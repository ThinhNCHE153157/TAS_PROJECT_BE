using Amazon.Runtime.Internal.Util;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;

namespace TAS.Application.Services
{
    public class TopicService : ITopicService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly ILogger<TopicService> _logger;

        public TopicService(IUnitOfWork unitOfWork, IMapper mapper , ILogger<TopicService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Topic> addTopic(AddTopicRequestDto request)
        {
            try
            {
                var topic =  _mapper.Map<Topic>(request);
                var result =  _unitOfWork.TopicRepository.AddTopic(topic);
                return topic;

            }
            catch (Exception)
            {
               _logger.LogError("Error in addTopic");
                return null;
            }
        }

        public async Task<List<GetTopicByCourseIdResponseDto>> getListTopicByCourseId(int courseId)
        {
            try
            {
                var topic = await _unitOfWork.TopicRepository.GetTopicByCourseId(courseId).Include(x => x.Videos).ToListAsync().ConfigureAwait(false);
                if (topic != null)
                {
                    var result = _mapper.Map<List<GetTopicByCourseIdResponseDto>>(topic);
                    return result;
                }
                return null;
            }
            catch (Exception)
            {
                _logger.LogError("Error in getListTopicByCourseId");
                return null;
            }
        }
    }
}
