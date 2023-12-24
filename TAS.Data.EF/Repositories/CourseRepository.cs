using Microsoft.EntityFrameworkCore;
using TAS.Data.Dtos.Responses;
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

        public IQueryable<Course> GetCourseById(int courseId)
        {
            return _context.Courses.Include(x => x.Topics).Where(x => x.CourseId == courseId);
        }

        public IQueryable<Course> GetCourseIdByName(string name)
        {
            return _context.Courses.Where(x => x.CourseName == name);
        }

        public List<int> GetListTopicIdByCourseId(int courseId)
        {
            return _context.Topics.Where(x => x.CourseId == courseId).Select(x => x.TopicId).ToList();
        }
    }
}
