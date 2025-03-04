namespace BookHaven.Application.Interfaces.Services;
public interface ICacheValidator
{
    Task<T> Validate<T>(string key, Func<Task<T>> valueProvider, TimeSpan timeSpan);
}