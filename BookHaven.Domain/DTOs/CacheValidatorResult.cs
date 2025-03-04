namespace BookHaven.Domain.DTOs;
public record class CacheValidatorResult<T>(bool IsValid, T Data);