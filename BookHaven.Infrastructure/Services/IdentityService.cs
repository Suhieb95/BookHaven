using System.Security.Claims;
using BookHaven.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace BookHaven.Infrastructure.Services;

public class IdentityService(IHttpContextAccessor IhttpContextAccessor) : IIdentityService
{
    private readonly IHttpContextAccessor _IhttpContextAccessor = IhttpContextAccessor;

    public Guid? GetCurrentLoggedInUser()
    {
        //The "D" format specifier stands for "32 digits separated by hyphens."
        if (Guid.TryParseExact(_IhttpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, "D", out Guid id))
            return id;

        return null;
    }
    public string GetCurrentLoggedInEmail()
       => _IhttpContextAccessor!.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value!;
}
