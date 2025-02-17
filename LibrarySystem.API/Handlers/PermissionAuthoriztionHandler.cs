using LibrarySystem.Application;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LibrarySystem.API.Handlers;
internal class PermissionAuthoriztionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissions = context.User
                                  .Claims
                                  .Where(c => c.Type == PermissionsClaim.Permissions)
                                  .Select(c => c.Value);

        if (permissions.Contains(requirement.Permission))
            context.Succeed(requirement);
        else
            context.Fail();

        return Task.CompletedTask;
    }
}