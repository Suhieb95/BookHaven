using LibrarySystem.Application.Interfaces.Services;

namespace LibrarySystem.Infrastructure.Authentication;
public class JwtTokenService : IJwtTokenService
{
    public async Task<string> GenerateAccessToken()
    {
        throw new NotImplementedException();
    }

    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }
}
