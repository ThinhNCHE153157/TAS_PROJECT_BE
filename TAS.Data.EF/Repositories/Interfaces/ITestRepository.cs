
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;
﻿using TAS.Data.Entities;


namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface ITestRepository : IBaseRepository<Test>
    {

        public IQueryable<Test> GetAllTest();
        public IQueryable<Test> GetTestById(int TestId);

        public IQueryable<Test> GetTestResultById(int testId);

    }
}
