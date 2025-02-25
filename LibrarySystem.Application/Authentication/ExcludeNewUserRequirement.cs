using Microsoft.AspNetCore.Authorization;
namespace LibrarySystem.Application.Authentication;
public class ExcludeNewUserRequirement(string roleToExclude) : IAuthorizationRequirement
{
    public string RoleToExclude => roleToExclude;
};