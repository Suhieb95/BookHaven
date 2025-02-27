using System.Data;
namespace BookHaven.Application.Interfaces.Database;
public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnection();
}