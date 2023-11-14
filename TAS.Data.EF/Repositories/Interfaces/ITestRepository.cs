using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface ITestRepository : IBaseRepository<Test>
    {
        public IQueryable<Test> GetTestResultById(int testId);
        public IQueryable<Test> GetTestById(int testId);
        public bool CreateTest(Test test);
        public bool UpdateTest(Test test);
        public bool UpdateStatusTest(int testId);

    }
}
