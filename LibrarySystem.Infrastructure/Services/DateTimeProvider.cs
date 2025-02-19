using LibrarySystem.Application.Interfaces;

namespace LibrarySystem.Infrastructure.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
