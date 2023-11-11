using TAS.Data.Dtos.Responses;

namespace TAS.Application.Services.Interfaces
{
    public interface ITestService
    {
        public Task<CourseResultResponseDto> CourseResult(int id);
    }
}
