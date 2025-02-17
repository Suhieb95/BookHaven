using Microsoft.AspNetCore.Authorization;
namespace LibrarySystem.Application.Authentication;
public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
