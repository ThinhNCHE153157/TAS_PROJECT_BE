using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.EF;

namespace TAS.Test.Mock
{
    public static class MockUnitOfWork
    {
        public static IUnitOfWork GetUnitOfWork()
        {
            var context = MockDbContext.GetDbContext();
            return new UnitOfWork(context);
        }
    }
}
