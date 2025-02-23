using Microsoft.AspNetCore.Authorization;
namespace LibrarySystem.Application;
public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission => permission;
}
