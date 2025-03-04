using BookHaven.Application.Interfaces.Services;
namespace BookHaven.Infrastructure.Services;
public class CacheValidator(IRedisCacheService redisCacheService) : ICacheValidator
{
    private readonly IRedisCacheService _redisCacheService = redisCacheService;
    public async Task<T> Validate<T>(string key, Func<Task<T>> valueProvider, TimeSpan timeSpan)
    {
        T? cachedValue = await _redisCacheService.Get<T>(key);
        if (cachedValue is not null) return cachedValue;

        T? newValue = await valueProvider();
        if (newValue is not null)
            await _redisCacheService.Set(key, newValue, timeSpan);

        return newValue;
    }
}