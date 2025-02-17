namespace LibrarySystem.Application.Interfaces;
public interface IDateTimeProvider
{
    static DateTime UtcNow { get; }
}
