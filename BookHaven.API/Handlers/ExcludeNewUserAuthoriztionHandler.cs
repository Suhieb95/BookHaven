using System.Security.Claims;

using BookHaven.Application.Authentication.AuthRequirements;
namespace BookHaven.API.Handlers;
public class ExcludeNewUserAuthoriztionHandler : AuthorizationHandler<ExcludeNewUserRequirement>
{
    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExcludeNewUserRequirement requirement)
    {
        bool isMatched = context.User.Identity?.IsAuthenticated == true && context.User.HasClaim(c => c.Type == ClaimTypes.Role);
        if (isMatched)
        {
            Claim? role = context.User.FindAll(ClaimTypes.Role).FirstOrDefault(c => c.Value == requirement.RoleToExclude);
            if (role is not null)
                context.Fail();
            else
                context.Succeed(requirement);
        }

        await Task.CompletedTask;
    }
}