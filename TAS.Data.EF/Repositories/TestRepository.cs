
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<Test> GetTestResultById(int testId)
        {
            return _context.Set<Test>().Include(x => x.TestResults).Where(x => x.TestId == testId);
        }
    }
}
