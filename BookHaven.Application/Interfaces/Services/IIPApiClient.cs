using BookHaven.Domain.DTOs.Auth;

namespace BookHaven.Application.Interfaces.Services;
public interface IIPApiClient
{
    Task<IPApiResponse?> Get(string? ipAddress, CancellationToken ct);
}