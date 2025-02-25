using System.Net.Http.Json;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.Entities;
using Microsoft.Extensions.Options;
namespace BookHaven.Infrastructure.Services;
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

