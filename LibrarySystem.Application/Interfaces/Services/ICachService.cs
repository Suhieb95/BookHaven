namespace LibrarySystem.Application.Interfaces.Services;
public interface ICacheService
{
    Task<T?> Get<T>(string key);
    Task<bool> Delete(string key);
    Task Set<T>(string key, T value, TimeSpan expiry);
}