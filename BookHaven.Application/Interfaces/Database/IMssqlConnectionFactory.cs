using System.Data;

namespace BookHaven.Application.Interfaces.Database;
public interface IMssqlConnectionFactory : IDbConnectionFactory<IDbConnection>;
