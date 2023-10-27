using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Application.Services.Interfaces
{
    public interface ICourseService
    {
        public Task<List<CourseHomepageResponeDto>> getCourseHomepage();
        public Task<List<CourseDashboardResponseDto>> GetAllCourse();
    }
}
