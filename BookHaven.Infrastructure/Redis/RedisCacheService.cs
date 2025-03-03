using System.Text.Json;

using BookHaven.Application.Interfaces.Services;

using StackExchange.Redis;

namespace BookHaven.Infrastructure.Redis;
public class RedisCacheService(IDatabase db) : IRedisCacheService
{
    private readonly IDatabase _db = db;

    public async Task<bool> Delete(string key)
        => !string.IsNullOrEmpty(key) && await _db.KeyDeleteAsync(key);
    public async Task<T?> Get<T>(string key)
    {
        if (string.IsNullOrEmpty(key)) return default;

        var cachedData = await _db.StringGetAsync(key);

        return cachedData.IsNullOrEmpty
            ? default
            : JsonSerializer.Deserialize<T>(cachedData!);
    }
    public async Task Set<T>(string key, T value, TimeSpan expiry)
    {
        if (string.IsNullOrEmpty(key) || value is null) return;
        var serialized = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(key, serialized, expiry);
    }
}