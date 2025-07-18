namespace AccessDesk_ASP_Server.Utilities.Constants
{
    public static class ErrorMessages
    {
        // Authentication errors
        public const string InvalidCredentials = "Invalid username/email or password";
        public const string UserNotFound = "User not found";
        public const string UserAlreadyExists = "User already exists";
        public const string InvalidToken = "Invalid token";
        public const string TokenExpired = "Token has expired";
        public const string UserNotActive = "User account is not active";

        // Registration errors
        public const string RegistrationFailed = "Registration failed";
        public const string EmailAlreadyExists = "Email already exists";
        public const string UsernameAlreadyExists = "Username already exists";

        // General errors
        public const string UnknownError = "An unknown error occurred";
        public const string ValidationError = "Validation failed";
        public const string UnauthorizedAccess = "Unauthorized access";
    }
}
