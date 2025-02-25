namespace LibrarySystem.API.Common.Constants;
internal static class Roles
{
    internal const string Admin = "Admin";
    internal const string Manager = "Manage";
    internal const string AdminOrManagerOnly = Admin + "," + Manager;
}
