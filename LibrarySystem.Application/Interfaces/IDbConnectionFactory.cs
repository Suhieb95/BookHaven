namespace LibrarySystem.Application.Interfaces;
public interface IDbConnectionFactory<T>
{
    Task<T> CreateConnection();
}
