namespace BookHaven.API.Common.Constants;
internal static class CustomRoles
{
    internal const string Admin = "Admin";
    internal const string Manager = "Manage";
    internal const string NewUser = "New User";
    internal const string AdminOrManagerOnly = Admin + "," + Manager;
}
