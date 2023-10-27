using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        public IQueryable<Course> GetAllCourses();
    }
}
