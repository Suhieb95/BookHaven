using System.Net.Http.Json;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.Entities;
using Microsoft.Extensions.Options;
namespace BookHaven.Infrastructure.Services;
public class IPApiClient(IOptions<IpProvider> ipProvider, IHttpClientFactory httpClient) : IIPApiClient
{
    private readonly IpProvider _IpProvider = ipProvider.Value;
    private readonly IHttpClientFactory _httpClient = httpClient;
    public async Task<IpApiResponse?> Get(string? ipAddress, CancellationToken ct)
    {
        HttpClient? client = _httpClient.CreateClient();
        client.BaseAddress = new Uri(_IpProvider.Provider);

        IpApiResponse? response = await client.GetFromJsonAsync<IpApiResponse>($"/json/{ipAddress}", ct);
        return response;
    }
}