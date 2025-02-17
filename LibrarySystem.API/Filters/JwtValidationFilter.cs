using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using static LibrarySystem.Application.Helpers.Extensions;
using static LibrarySystem.API.Common.Constants.ApiEndPoints;

namespace LibrarySystem.API.Filters;
public class JwtValidationFilter(IOptions<JwtSettings> jwtSettings) : IActionFilter
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    public void OnActionExecuting(ActionExecutingContext context)
    {
        bool isAnonymous = context.ActionDescriptor.EndpointMetadata
               .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
        bool isRefreshTokenPath = context.HttpContext.Request.Path.StartsWithSegments(Auth.RefreshToken);

        if (isAnonymous || isRefreshTokenPath)
            return;

        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader) ||
                        !authHeader.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return;

        string jwtToken = authHeader.ToString()["Bearer ".Length..].Trim();

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = ValidateJwtToken(_jwtSettings);
            tokenHandler.ValidateToken(jwtToken, validationParameters, out _);
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = "Invalid or expired token.",
                Type = "https://httpstatuses.com/401",
                Instance = context.HttpContext?.Request.Path
            });
        }
    }
    public void OnActionExecuted(ActionExecutedContext context) { }
}