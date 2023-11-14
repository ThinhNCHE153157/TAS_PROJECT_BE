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
using TAS.Data.Entities;

namespace TAS.Application.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly ILogger<TestService> _logger;
        public TestService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TestService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CourseResultResponseDto> CourseResult(int id)
        {
            try
            {
                var test = await _unitOfWork.TestRepository.GetTestResultById(id).FirstOrDefaultAsync().ConfigureAwait(false);
                if (test != null)
                {
                    var result = _mapper.Map<CourseResultResponseDto>(test);
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<GetTestByIdResponseDto> GetTestById(int id)
        {
            try
            {
                var test = await _unitOfWork.TestRepository.GetTestById(id).FirstOrDefaultAsync().ConfigureAwait(false);
                if (test != null)
                {
                    var result = _mapper.Map<GetTestByIdResponseDto>(test);
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> UpdateTest(UpdateTestRequestDto request)
        {
            try
            {
                var test = _unitOfWork.TestRepository.GetTestById(request.TestId).FirstOrDefault();
                if (test != null)
                {

                    test.TestName = request.TestName;
                    test.TestTotalScore = request.TestTotalScore;
                    test.TestDuration = request.TestDuration;
                    test.TestDescription = request.TestDescription;
                    var result = _unitOfWork.TestRepository.UpdateTest(test);
                    return result;
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateStatusTest(int id)
        {
            try
            {
                var result = _unitOfWork.TestRepository.UpdateStatusTest(id);
                return result;
            }catch(Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> CreateTestForCourse(CreateTestForCourseRequestDto request)
        {
            try
            {
                var test = _mapper.Map<Test>(request.Tests);
                var result = _unitOfWork.TestRepository.CreateTestForCourse(request.CourseId,test);
                return result;
            }catch(Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
}
