namespace LibrarySystem.API.Common.Constants;
internal static class ApiEndPoints
{
    internal const string BaseController = "/api/v1/[controller]";
    internal static class Auth
    {
        private const string Authentication = "/auth";
        internal const string RefreshToken = Authentication + "/refresh-token";
    }
    internal class Books
    {
        internal const string GetById = BaseController + "/{id}";
    }
}