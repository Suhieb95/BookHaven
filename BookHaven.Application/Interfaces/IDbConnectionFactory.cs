using System.Data;
namespace BookHaven.Application.Interfaces;
public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnection();
}