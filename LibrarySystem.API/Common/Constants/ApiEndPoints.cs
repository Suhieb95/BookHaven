namespace LibrarySystem.API.Common.Constants;
internal static class ApiEndPoints
{
    internal const string BaseController = "/api/v1/[controller]";
    internal class Auth
    {
        private const string Authentication = "/auth";
        internal const string RefreshToken = Authentication + "/refresh-token";
    }
    internal class Books
    {
        internal const string GetById = BaseController + "/{id}";
    }
    internal class Person
    {
        internal const string ConfirmEmailAddress = BaseController + "/confirm-email/{id}";
        internal const string Register = BaseController + "/register";
        internal const string Login = BaseController + "/login";
        internal const string ChangePassword = BaseController + "/change-password";
        internal const string VerifyResetPasswordToken = BaseController + "/verify-password-reset-token";
        internal const string ResetPasswordRequest = BaseController + "/reset-password-request";
    }
}