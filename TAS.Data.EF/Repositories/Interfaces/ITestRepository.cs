using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface ITestRepository : IBaseRepository<Test>
    {
        public IQueryable<Test> GetTestResultById(int testId);
    }
}
