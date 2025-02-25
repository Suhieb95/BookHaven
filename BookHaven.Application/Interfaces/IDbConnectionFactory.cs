namespace BookHaven.Application.Interfaces;
public interface IDbConnectionFactory<T>
{
    Task<T> CreateConnection();
}
