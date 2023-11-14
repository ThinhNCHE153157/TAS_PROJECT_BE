using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Requests;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories
{
    public class TestRepository : BaseRepository<Test>, ITestRepository
    {
        public TestRepository(TASContext context) : base(context)
        {
        }

        public bool CreateTestForCourse(int courseId, Test test)
        {
            var course = _context.Courses.Where(x => x.CourseId == courseId).FirstOrDefault();
            if (course != null)
            {
                course.Tests.Add(test);
                int n = _context.SaveChanges();
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool UpdateStatusTest(int testId)
        {
            var find = _context.Set<Test>().Where(x => x.TestId == testId).FirstOrDefault();
            if (find != null)
            {
                if (find.IsDeleted == true)
                {
                    find.IsDeleted = false;
                }
                else
                {
                    find.IsDeleted = true;
                }
                _context.Update(find);
                int n = _context.SaveChanges();
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }


        public IQueryable<Test> GetTestById(int testId)
        {
            return _context.Set<Test>().Where(x => x.TestId == testId);
        }

        public IQueryable<Test> GetTestResultById(int testId)
        {
            return _context.Set<Test>().Include(x => x.TestResults).Where(x => x.TestId == testId);
        }

        public bool UpdateTest(Test test)
        {
            _context.Update(test);
            int n = _context.SaveChanges();
            if (n > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
