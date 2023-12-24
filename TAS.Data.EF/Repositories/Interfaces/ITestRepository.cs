using TAS.Data.Dtos.Requests;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface ITestRepository : IBaseRepository<Test>
    {
        public IEnumerable<Test> GetAllTest();
        public IQueryable<Test> GetTestResultById(int testId);
        public IQueryable<Test> GetTestById(int testId);
        public bool UpdateTest(Test test);
        public bool UpdateStatusTest(int testId);
        public IQueryable<Test> GetListTestFree();
        public IEnumerable<int> GetPartsByTestId(int testId);
        public IEnumerable<int> GetPartsAudioByTestId(int testId);
        public IEnumerable<int> GetPartsReadByTestId(int testId);
        public bool SaveTestResult(TestResult request);
        public int GetTestResultId(int testId, int accountId);
        public TestResult GetTestResult(int testResultId);
        public int GetPartIdByTopicId(int topicId);
        public List<Part> GetPartByTestId(int testId);
        public List<int> GetTestIdByTopicId (int topicId);
        public bool AddPart(Part part);
    }
}
