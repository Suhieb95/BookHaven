using LibrarySystem.Domain.DTOs.Auth;

namespace LibrarySystem.Application.Interfaces.Services;
public interface IIPApiClient
{
    Task<IpApiResponse?> Get(string? ipAddress, CancellationToken ct);
}