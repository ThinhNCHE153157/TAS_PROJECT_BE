using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(TASContext context) : base(context)
        {
        }

        public IQueryable<Course> GetAllCourses()
        {
           return _context.Set<Course>();
        }
    }
}
