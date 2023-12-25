using TAS.Data.Dtos.Responses;
using TAS.Data.Dtos.Requests;
using TAS.Data.Entities;

namespace TAS.Application.Services.Interfaces
{
    public interface ICourseService
    {
        public Task<List<CourseHomepageResponeDto>> getCourseHomepage();
        public Task<List<CourseDashboardResponseDto>> GetAllCourse();
        public Task<GetCourseByIdResponseDto> GetCourseById(int id);
        public Task<bool> AddCourse(AddCourseRequestDto request);
        public Task<bool> UpdateStatus(int courseId, int status);
        public Task<bool> UpdateCost(UpdateCostRequestDto request);
        public Task<int> GetCourseIdByName(string name);
        public Task<List<int?>> GetCourseByAccountId(int accountId);
        public Task<List<GetEnterpriseCourseResponseDto>> GetCourseByEnterpriseName(string name);
        public string GetEnterpriseNameByAccountId(int id);
        public Task<List<Course>> GetListCourseByAccountId(int id);
    }
}
