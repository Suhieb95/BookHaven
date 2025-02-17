using System.Data;
namespace LibrarySystem.Application.Interfaces;
public interface IMssqlDbTransaction : IDisposable, ISqlDBTransaction
{
    Task<List<T>> LoadDataInTransaction<T>(string storedProcedure, object? parameters = default, CommandType? commandType = null);
    Task SaveDataInTransaction<P>(string storedProcedure, P parameters, CommandType? commandType = null, CancellationToken? cancellationToken = null);
    ///<returns>Object of type T.</returns>
    Task<T?> SaveDataInTransaction<T>(string storedProcedure, object parameters, CommandType? commandType = null, CancellationToken? cancellationToken = null);
    ///<returns>Object of type T.</returns>
    Task<T?> LoadSingleInTransaction<T>(string storedProcedure, object? parameters = default, CommandType? commandType = null);

}
