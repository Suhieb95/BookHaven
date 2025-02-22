using System.Security.Claims;
using LibrarySystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Infrastructure.Services;

public class IdentityService(IHttpContextAccessor _IhttpContextAccessor) : IIdentityService
{
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
