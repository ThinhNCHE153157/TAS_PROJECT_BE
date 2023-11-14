using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;

namespace TAS.Application.Services.Interfaces
{
    public interface ITestService
    {
        public Task<CourseResultResponseDto> CourseResult(int id);
        public Task<GetTestByIdResponseDto> GetTestById(int id);
        public Task<bool> UpdateTest(UpdateTestRequestDto request);
        public Task<bool> UpdateStatusTest(int id);
        public Task<bool> CreateTestForCourse(CreateTestForCourseRequestDto request);
    }
}
