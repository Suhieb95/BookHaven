using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

using System.IdentityModel.Tokens.Jwt;

using static BookHaven.Application.Helpers.Extensions;

namespace BookHaven.API.Filters;
public class JwtValidationFilter(IOptions<JwtSettings> jwtSettings) : IAsyncActionFilter
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    // ActionExecutingContext contains information about the current HTTP request, the action that is about to be executed
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        bool isAnonymous = context.ActionDescriptor.EndpointMetadata
               .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
        bool isRefreshTokenPath = context.HttpContext.Request.Path.StartsWithSegments(Person.RefreshToken);

        if (isAnonymous || isRefreshTokenPath)
        {
            await next();
            return;
        }

        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader) ||
                        !authHeader.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            await next();
            return;
        }

        string jwtToken = authHeader.ToString()["Bearer ".Length..].Trim(); // Remove the "Bearer " part From the token.

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = ValidateJwtToken(_jwtSettings);
            tokenHandler.ValidateToken(jwtToken, validationParameters, out _);
            await next();
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ProblemDetails()
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = "Invalid or expired token.",
                Type = "https://httpstatuses.com/401",
                Instance = context.HttpContext?.Request.Path
            });
        }
    }
}