using LibrarySystem.Application.Interfaces.Database;
using Microsoft.Data.SqlClient;
namespace LibrarySystem.Infrastructure.DataAccess;
public class MssqlConnectionFactory(string connectionString) : IMssqlConnectionFactory
{
    private readonly string _connectionString = connectionString;
    public async Task<IDbConnection> CreateConnection()
    {
        SqlConnection connection = new (_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
