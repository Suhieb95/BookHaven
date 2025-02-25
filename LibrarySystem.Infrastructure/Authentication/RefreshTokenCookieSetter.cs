using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LibrarySystem.Infrastructure.Authentication;
public class RefreshTokenCookieSetter(IDateTimeProvider _dateTimeProvider, IOptions<RefreshJwtSettings> refreshJwtSettings) : IRefreshTokenCookieSetter
{
    private readonly RefreshJwtSettings _refreshJwtSettings = refreshJwtSettings.Value;
    public void SetJwtTokenCookie(HttpContext httpContext, string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None, // Allow cross-site usage
            Expires = _dateTimeProvider.UtcNow.AddDays(_refreshJwtSettings.ExpiryDays),
            // Domain = ".runasp.net" // Accessible to all subdomains
        };

        httpContext.Response.Cookies.Append("refreshToken", token, cookieOptions);
        httpContext.Response.Headers.CacheControl = "public, max-age=1209600";
    }
    public void DeleteJwtTokenCookie(HttpContext httpContext, string cookieName)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None, // Allow cross-site usage
            Expires = _dateTimeProvider.UtcNow.AddDays(-20),
            // Domain = ".runasp.net" // Accessible to all subdomains
        };

        httpContext.Response.Cookies.Delete(cookieName, cookieOptions);
    }
}
