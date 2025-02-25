using Microsoft.AspNetCore.Authorization;
namespace BookHaven.Application.Authentication;
public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}