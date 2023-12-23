using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;

namespace TAS.Application.Services.Interfaces
{
    public interface ITestService
    {
        public Task<CourseResultResponseDto> CourseResult(int id);
        public Task<GetTestByIdResponseDto> GetTestById(int id);
        public Task<List<GetAllTestResponseDto>> GetAllTest();
        public Task<bool> UpdateTest(UpdateTestRequestDto request);
        public Task<bool> UpdateStatusTest(int id);
        public Task<bool> CreateTestForCourse(CreateTestForCourseRequestDto request);
        public Task<List<GetListTestFreeResponseDto>> getListTestFreeResponseDtos();
        public Task<GetListPartOfTestResponseDto> getListPartOfTest(int testId);
        public Task<bool> SaveTestResult(SaveTestResultRequestDto request);
        public Task<SaveTestResultResponseDto> TestDetail(int testId, int accountId);
        public int GetPartIdByTopicId(int courseId);
    }
}
