namespace TAS.Infrastructure.Constants
{
    public class ApiRouter
    {
        public static class CommonRouter
        {
            private const string BaseUrl = "~/api/v1";
            public const string CommonLogin = $"{BaseUrl}/common/login";
            public const string CommonLogout = $"{BaseUrl}/common/logout";
            public const string CommonRegister = $"{BaseUrl}/common/register";
            public const string CommonRefreshToken = $"{BaseUrl}/common/refresh-token";
            public const string CommonProfile = $"{BaseUrl}/common/profile";
        }

        public static class PublicRouter
        {
            private const string BaseUrl = "~/api/v1";
        }

        public static class AdminRouter
        {
            private const string BaseUrl = "~/api/v1";
        }
    }
}
