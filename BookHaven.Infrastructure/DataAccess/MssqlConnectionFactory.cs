using BookHaven.Application.Interfaces.Services;
using Microsoft.Data.SqlClient;
namespace BookHaven.Infrastructure.DataAccess;
public class MSSQLConnectionFactory(string connectionString) : IMSSQLConnectionFactory
{
    private readonly string _connectionString = connectionString;
    public async Task<IDbConnection> CreateConnection()
    {
        SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}