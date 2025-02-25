using BookHaven.Domain.DTOs;

namespace BookHaven.Application.Interfaces.Services;

public interface IRefreshTokenValidator
{
    Task<Result<RefreshToken>> ValidateRefreshToken(string? token, CancellationToken cancellationToken);
}
