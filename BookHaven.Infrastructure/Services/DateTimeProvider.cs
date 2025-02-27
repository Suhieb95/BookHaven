using BookHaven.Application.Interfaces.Services;

namespace BookHaven.Infrastructure.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
