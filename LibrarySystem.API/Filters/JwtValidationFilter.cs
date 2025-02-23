using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using static LibrarySystem.Application.Helpers.Extensions;

namespace LibrarySystem.API.Filters;
public class JwtValidationFilter(IOptions<JwtSettings> jwtSettings) : IActionFilter
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    public void OnActionExecuting(ActionExecutingContext context) // ActionExecutingContext contains information about the current HTTP request, the action that is about to be executed
    {
        bool isAnonymous = context.ActionDescriptor.EndpointMetadata
               .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
        bool isRefreshTokenPath = context.HttpContext.Request.Path.StartsWithSegments(Auth.RefreshToken);

        if (isAnonymous || isRefreshTokenPath)
            return;

        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader) ||
                        !authHeader.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return;

        string jwtToken = authHeader.ToString()["Bearer ".Length..].Trim(); // Remove the "Bearer " part From the token.

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = ValidateJwtToken(_jwtSettings);
            tokenHandler.ValidateToken(jwtToken, validationParameters, out _);
        }
        catch
        {
            ProblemDetails problem = new()
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = "Invalid or expired token.",
                Type = "https://httpstatuses.com/401",
                Instance = context.HttpContext?.Request.Path
            };
            context.Result = new UnauthorizedObjectResult(problem);
        }
    }
    public void OnActionExecuted(ActionExecutedContext context) { }
}