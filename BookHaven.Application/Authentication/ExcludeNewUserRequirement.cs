using Microsoft.AspNetCore.Authorization;
namespace BookHaven.Application.Authentication;
public class ExcludeNewUserRequirement(string roleToExclude) : IAuthorizationRequirement
{
    public string RoleToExclude => roleToExclude;
};