using LibrarySystem.Application.Interfaces;

namespace LibrarySystem.Infrastructure.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public static DateTime UtcNow => DateTime.UtcNow;
}
