using System.Data;

namespace LibrarySystem.Application.Interfaces.Database;
public interface IMssqlConnectionFactory : IDbConnectionFactory<IDbConnection>;
