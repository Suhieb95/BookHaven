using BookHaven.Domain.DTOs.Auth;

namespace BookHaven.Application.Interfaces.Services;
public interface IIPApiClient
{
    Task<IpApiResponse?> Get(string? ipAddress, CancellationToken ct);
}