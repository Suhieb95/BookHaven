namespace LibrarySystem.Application.Interfaces.Services;
public interface IJwtTokenService
{
    Task<string> GenerateAccessToken();
    string GenerateRefreshToken();
}