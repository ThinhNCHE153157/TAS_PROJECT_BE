using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;

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
        public async Task<List<TestDashboardResponseDto>> GetAllTest()
        {
            try
            {
                var listTest = await _unitOfWork.TestRepository.GetAllTest().ToListAsync().ConfigureAwait(false);
                var result = _mapper.Map<List<TestDashboardResponseDto>>(listTest);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<GetTestByIdResponseDto> GetTestById(int id)
        {
            try
            {
                var test = await _unitOfWork.TestRepository.GetTestById(id).Include(x => x.Courses).FirstOrDefaultAsync().ConfigureAwait(false);
                var result = _mapper.Map<GetTestByIdResponseDto>(test);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return null;
        }

        public async Task<List<TestHomepageResponeDto>> GetTestHomepage()
        {
            try
            {
                var listTest = await _unitOfWork.TestRepository.GetAllTest().ToListAsync().ConfigureAwait(false);
                var result = _mapper.Map<List<TestHomepageResponeDto>>(listTest);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }


            
        }
        public async Task<CourseResultResponseDto> CourseResult(int id)
            {
                var test = await _unitOfWork.TestRepository.GetTestResultById(id).FirstOrDefaultAsync().ConfigureAwait(false);
                var result = _mapper.Map<CourseResultResponseDto>(test);
                return result;
            }
    }
}