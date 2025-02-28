using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace BookHaven.Application.Authentication.AuthRequirements;
public class PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
{
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

        return policy is not null ? policy : new AuthorizationPolicyBuilder().AddRequirements(new PermissionRequirement(policyName))
                                                                            .Build();
    }
}