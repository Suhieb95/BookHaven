using System.Net.Http.Json;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.Entities;
using Microsoft.Extensions.Options;
namespace LibrarySystem.Infrastructure.Services;
public class IPApiClient : IIPApiClient
{
    private readonly IpProvider _IpProvider;
    private readonly HttpClient _httpClient;
    public IPApiClient(IOptions<IpProvider> ipProvider, HttpClient httpClient)
        => (_IpProvider, _httpClient) = (ipProvider.Value, httpClient);
    public async Task<IpApiResponse?> Get(string? ipAddress, CancellationToken ct)
    {
        var route = $"{_IpProvider.Provider}/json/{ipAddress}";
        var response = await _httpClient.GetFromJsonAsync<IpApiResponse>(route, ct);
        return response;
    }
}

