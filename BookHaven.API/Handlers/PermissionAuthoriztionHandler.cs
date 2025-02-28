using BookHaven.Application.Authentication.AuthRequirements;
namespace BookHaven.API.Handlers;
internal class PermissionAuthoriztionHandler : AuthorizationHandler<PermissionRequirement>
{
    /*
     AuthorizationHandlerContext is responsible for evaluating whether a user or principal meets a specific authorization requirement (such as roles, claims, etc.) to perform an action.
     It's used in the middle of the authorization process, before the request is fully authorized or denied
    */
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