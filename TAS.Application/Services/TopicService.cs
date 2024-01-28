using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;
using TAS.Data.Entities;
using TAS.Infrastructure.Constants;

namespace TAS.Application.Services
{
    public class TopicService : ITopicService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly ITestService _testService;
        public readonly ILogger<TopicService> _logger;

        public TopicService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TopicService> logger, ITestService testService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _testService = testService;
            _logger = logger;
        }
        public async Task<Topic> addTopic(AddTopicRequestDto request)
        {
            try
            {
                var topic = _mapper.Map<Topic>(request);
                var result = _unitOfWork.TopicRepository.AddTopic(topic);
                return topic;

            }
            catch (Exception)
            {
                _logger.LogError("Error in addTopic");
                return null;
            }
        }
        public async Task<List<Topic>> addListTopic(List<AddTopicRequestDto> request)
        {
            try
            {
                var topic = _mapper.Map<List<Topic>>(request);
                var result = _unitOfWork.TopicRepository.AddListTopic(topic);
                return topic;

            }
            catch (Exception)
            {
                _logger.LogError("Error in addListTopic");
                return null;
            }
        }

        public async Task<List<GetTopicByCourseIdResponseDto>> getListTopicByCourseId(int courseId)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseById(courseId).FirstOrDefaultAsync().ConfigureAwait(false);
                if (course != null)
                {
                    var topic = await _unitOfWork.TopicRepository.GetTopicByCourseId(courseId).Include(x => x.Videos).ToListAsync().ConfigureAwait(false);
                    if (topic != null)
                    {
                        foreach (var item in topic)
                        {
                            foreach (var test in item.Tests)
                            {
                                if (test.IsDeleted==Common.IsDelete)
                                {
                                    item.Tests.Remove(test);
                                }
                            }
                        }
                        var result = _mapper.Map<List<GetTopicByCourseIdResponseDto>>(topic);
                        foreach (var item in result)
                        {
                            item.CourseName = course.CourseName;
                            var x = _testService.GetPartIdByTopicId(item.TopicId);
                            if (x != 0)
                            {
                                item.PartId = x;
                            }

                        }
                        return result;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                _logger.LogError("Error in getListTopicByCourseId");
                return null;
            }
        }

        public async Task<Topic> GetTopicByName(string name)
        {
            try
            {
                var result = _unitOfWork.TopicRepository.GetTopicByName(name);
                return result;
            }
            catch (Exception)
            {
                _logger.LogError("Error in GetTopicByName");
                return null;
            }
        }
    }
}
