namespace PrTask.Api.Contracts.V1
{
#pragma warning disable 1591
    public class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        private const string Base = Root + "/" + Version;
        
        public static class Authentication
        {
            public const string Register = Base + "/auth/register";
            public const string Login = Base + "/auth/login";
            public const string Logout = Base + "/auth/logout";
            public const string RefreshToken = Base + "/auth/refresh-token";
        }
    }
}