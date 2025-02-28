using Microsoft.AspNetCore.Authorization;
namespace BookHaven.Application.Authentication.AuthRequirements;
public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}