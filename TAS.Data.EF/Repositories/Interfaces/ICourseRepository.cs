using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        public IQueryable<Course> GetAllCourses();
        public IQueryable<Course> GetCourseById(int courseId);
        public IQueryable<Course> GetCourseIdByName(string name);
    }
}
