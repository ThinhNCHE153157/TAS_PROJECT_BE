using Microsoft.Extensions.Configuration;

namespace TAS.Infrastructure.Configurations
{
    public class AppSettings
    {
        private static IConfiguration Configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
           .AddEnvironmentVariables()
           .Build();

        public static string Connection
        {
            get { return Configuration["ConnectionStrings:Default"]; }
        }

        public static string Redis
        {
            get { return Configuration["ConnectionStrings:Redis"]; }
        }

        public static string SaltKey
        {
            get { return Configuration["SaltKey"]; }
        }

        public static string ValidAudience
        {
            get { return Configuration["Jwt:ValidAudience"]; }
        }

        public static string ValidIssuer
        {
            get { return Configuration["Jwt:ValidIssuer"]; }
        }

        public static string JwtAccessTokenSecret
        {
            get { return Configuration["Jwt:AccessTokenSecret"]; }
        }

        public static string RefreshTokenSecret
        {
            get { return Configuration["Jwt:RefreshTokenSecret"]; }
        }

        public static int AccessTokenExpriedTime
        {
            get { return Convert.ToInt32(Configuration["Jwt:AccessTokenExpriedTimeInMinutes"]); }
        }

        public static int RefreshTokenExpriedTime
        {
            get { return Convert.ToInt32(Configuration["Jwt:RefreshTokenExpriedTimeInDays"]); }
        }

        public static int PageSize
        {
            get { return Convert.ToInt32(Configuration["Pagination:Size"]); }
        }

        public static int PageRange
        {
            get { return Convert.ToInt32(Configuration["Pagination:NavRange"]); }
        }

        public static string ObjectStorageAccessId
        {
            get { return Configuration["ObjectStorage:AccessId"]; }
        }

        public static string ObjectStorageAccessKey
        {
            get { return Configuration["ObjectStorage:AccessKey"]; }
        }

        public static string ObjectStorageURL
        {
            get { return Configuration["ObjectStorage:URL"]; }
        }

        public static string ObjectStorageBucket
        {
            get { return Configuration["ObjectStorage:Bucket"]; }
        }

        public static string OmdbUrl
        {
            get { return Configuration["Omdb:Url"]; }
        }
        public static string OmdbKey
        {
            get { return Configuration["Omdb:Key"]; }
        }

        public static string OmdbName
        {
            get { return Configuration["Omdb:Name"]; }
        }
    }
}
