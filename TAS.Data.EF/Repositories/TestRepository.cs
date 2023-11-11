using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories
{
    public class TestRepository : BaseRepository<Test>, ITestRepository
    {
        public TestRepository(TASContext context) : base(context)
        {
        }

        public IQueryable<Test> GetAllTest()
        {
            return _context.Set<Test>();
        }

        public IQueryable<Test> GetTestById(int TestId)
        {
            throw new NotImplementedException();
        }

    }
}
