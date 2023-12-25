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

        public List<int?> GetCourseIdByAccountId(int accountId)
        {
            return _context.Orders.Where(x => x.AccountId == accountId).Select(x => x.CourseId).ToList();
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

        public List<Course> GetCourseByEnterpriseName(string name)
        {
            return _context.Courses.Where(x => x.CreateUser == name).ToList();
        }

        public List<Course> GetListCourseByAccountId(int id)
        {
            var listid =  GetCourseIdByAccountId(id);
            List<Course> list = new List<Course>();
            foreach (var item in listid)
            {
                var course = _context.Courses.Where(x => x.CourseId == item).FirstOrDefault();
                if (course!=null)
                {
                list.Add(course);
                }
            }
            return list;
        }
    }
}
