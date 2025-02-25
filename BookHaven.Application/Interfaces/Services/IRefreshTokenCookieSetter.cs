using Microsoft.AspNetCore.Http;

namespace BookHaven.Application.Interfaces.Services;
public interface IRefreshTokenCookieSetter
{
    void SetJwtTokenCookie(HttpContext httpContext, string token);
    void DeleteJwtTokenCookie(HttpContext httpContext, string cookieName);
}
