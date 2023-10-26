using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TAS.Data.Entities;

namespace TAS.Data.EF
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TASContext>
    {
        public TASContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("Default");
            var builder = new DbContextOptionsBuilder<TASContext>();
            builder.UseSqlServer(connectionString);
            return new TASContext(builder.Options);
        }
    }
}
