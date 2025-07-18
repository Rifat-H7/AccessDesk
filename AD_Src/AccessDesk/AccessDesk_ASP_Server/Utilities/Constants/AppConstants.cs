namespace AccessDesk_ASP_Server.Utilities.Constants
{
    public static class AppConstants
    {
        public static class Roles
        {
            public const string Admin = "Admin";
            public const string User = "User";
        }

        public static class ClaimTypes
        {
            public const string FullName = "FullName";
            public const string UserId = "UserId";
        }

        public static class AuthPolicies
        {
            public const string AdminOnly = "AdminOnly";
            public const string UserOrAdmin = "UserOrAdmin";
        }
    }
}
