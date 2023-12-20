using TAS.Data.Entities;

namespace TAS.Test.Mock
{
    public static class MockDbContext
    {
        public static TASContext GetDbContext()
        {
            return new TASContext();
        }
    }
}
