using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace BookHaven.API.Filters;
public class UserAgentFilter(IWebHostEnvironment _env, IOptions<AllowedAgent> allowedAgent) : IAsyncActionFilter
{
    private readonly AllowedAgent _allowedAgent = allowedAgent.Value;
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (_env.IsDevelopment())
        {
            await next();
            return;
        }

        string? agent = context.HttpContext.Request.Headers.UserAgent.ToString();
        if (IsInvalidAgent(agent))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
    private bool IsInvalidAgent(string? agent)
        => string.IsNullOrEmpty(agent) || !_allowedAgent.Agents.Any(curr => curr.Contains(agent, StringComparison.CurrentCultureIgnoreCase));
}