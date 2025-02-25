using LibrarySystem.Domain.DTOs;

namespace LibrarySystem.Application.Interfaces.Services;

public interface IRefreshTokenValidator
{
    Task<Result<RefreshToken>> ValidateRefreshToken(string? token, CancellationToken cancellationToken);
}
