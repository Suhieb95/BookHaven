using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.Specification;

namespace BookHaven.Infrastructure.DataAccess;
internal sealed class SqlDataAccess(IMSSQLConnectionFactory idbConnectionFactory) : ISqlDataAccess, IMssqlDbTransaction
{
    private IDbConnection? _connection;
    private IDbTransaction? _transaction;
    private readonly IMSSQLConnectionFactory _IdbConnectionFactory = idbConnectionFactory;
    public async Task<List<T>> LoadData<T>(Specification<T> specification, CancellationToken? cancellationToken = null)
    {
        CancellationToken ct = cancellationToken ?? CancellationToken.None;
        using IDbConnection connection = await _IdbConnectionFactory.CreateConnection();

        var rows = await connection.QueryAsync<T>(new CommandDefinition(specification.ToSql(), specification.Parameters,
            commandType: specification.CommandType, cancellationToken: ct));
        return rows.AsList();
    }
    public async Task<T?> LoadFirstOrDefault<T>(Specification<T> specification, CancellationToken? cancellationToken = null)
    {
        CancellationToken ct = cancellationToken ?? CancellationToken.None;
        using IDbConnection connection = await _IdbConnectionFactory.CreateConnection();

        T? row = await connection.QueryFirstOrDefaultAsync<T>(new CommandDefinition(specification.ToSql(), specification.Parameters,
            commandType: specification.CommandType, cancellationToken: ct));
        return row;
    }
    public async Task<List<T>> LoadData<T>(string sql, object? parameters = default, CommandType? commandType = null, CancellationToken? cancellationToken = null)
    {
        CancellationToken ct = cancellationToken ?? CancellationToken.None;
        using IDbConnection connection = await _IdbConnectionFactory.CreateConnection();

        var rows = await connection.QueryAsync<T>(new CommandDefinition(sql, parameters,
            commandType: commandType, cancellationToken: ct));
        return rows.AsList();
    }
    public async Task<T?> LoadFirstOrDefault<T>(string sql, object? parameters = default, CommandType? commandType = null, CancellationToken? cancellationToken = null)
    {
        CancellationToken ct = cancellationToken ?? CancellationToken.None;
        using IDbConnection connection = await _IdbConnectionFactory.CreateConnection();
        T? row = await connection.QueryFirstOrDefaultAsync<T>(new CommandDefinition(sql, parameters, commandType: commandType, cancellationToken: ct));
        return row;
    }
    public async Task SaveData<P>(string sql, P? parameters = default, CommandType? commandType = null, CancellationToken? cancellationToken = null)
    {
        CancellationToken ct = cancellationToken ?? CancellationToken.None;
        using IDbConnection connection = await _IdbConnectionFactory.CreateConnection();
        await connection.ExecuteScalarAsync(new CommandDefinition(sql, parameters,
            commandType: commandType, cancellationToken: ct));
    }
    public async Task<T?> SaveData<T>(string sql, object? parameters = default, CommandType? commandType = null, CancellationToken? cancellationToken = null)
    {
        CancellationToken ct = cancellationToken ?? CancellationToken.None;
        using IDbConnection connection = await _IdbConnectionFactory.CreateConnection();
        var result = await connection.ExecuteScalarAsync<T>(new CommandDefinition(sql, parameters, commandType: commandType, cancellationToken: ct));
        return result;
    }
    public async Task<(List<T1>, List<T2>)> FetchTwoListsAsync<T1, T2>(string sql, CancellationToken? cancellationToken = null, object? param = default, CommandType? commandType = null)
        => await ExecuteQueryAsync(
            sql,
            param,
            commandType,
            async result => (await result.ReadAsync<T1>()).AsList(),
            async result => (await result.ReadAsync<T2>()).AsList(),
            cancellationToken);
    // Fetch a single item and a list
    public async Task<(T1?, List<T2>)> FetchSingleAndListAsync<T1, T2>(string sql, CancellationToken? cancellationToken = null, object? param = default, CommandType? commandType = null)
        => await ExecuteQueryAsync(
            sql,
            param,
            commandType,
            async result => await result.ReadFirstOrDefaultAsync<T1>(),
            async result => (await result.ReadAsync<T2>()).AsList(),
            cancellationToken);

    // Fetch a list and a single item
    public async Task<(List<T1>, T2?)> FetchListAndSingleAsync<T1, T2>(string sql, CancellationToken? cancellationToken = null, object? param = default, CommandType? commandType = null)
        => await ExecuteQueryAsync(
            sql,
            param,
            commandType,
            async result => (await result.ReadAsync<T1>()).AsList(),
            async result => await result.ReadFirstOrDefaultAsync<T2>(),
            cancellationToken);
    public async Task<List<T>> LoadDataInTransaction<T>(string sql, object? parameters = default, CommandType? commandType = null)
    {
        IEnumerable<T> rows = await _transaction!.Connection!.QueryAsync<T>(sql, parameters, commandType: commandType, transaction: _transaction);
        return rows.AsList();
    }
    public async Task<T?> LoadSingleInTransaction<T>(string sql, object? parameters = default, CommandType? commandType = null)
       => await _transaction!.Connection!.QuerySingleOrDefaultAsync<T>(sql, parameters, commandType: commandType, transaction: _transaction);
    public async Task SaveDataInTransaction<P>(string sql, P parameters, CommandType? commandType = null, CancellationToken? cancellationToken = null)
        => await _transaction!.Connection!.ExecuteAsync(new CommandDefinition(sql, parameters,
       commandType: commandType, transaction: _transaction, cancellationToken: cancellationToken ?? CancellationToken.None));
    public async Task<T?> SaveDataInTransaction<T>(string sql, object parameters, CommandType? commandType = null, CancellationToken? cancellationToken = null)
        => await _transaction!.Connection!.ExecuteScalarAsync<T>(new CommandDefinition(sql, parameters,
       commandType: commandType, transaction: _transaction, cancellationToken: cancellationToken ?? CancellationToken.None));
    public void CommitTransaction()
    {
        _transaction?.Commit();
        Dispose();
    }
    public void RollbackTransaction()
    {
        _transaction?.Rollback();
        Dispose();
    }
    public async Task InitilizeTransaction(IsolationLevel? isolationLevel = null)
    {
        _connection = await _IdbConnectionFactory.CreateConnection();
        _transaction = _connection.BeginTransaction(isolationLevel ?? IsolationLevel.ReadCommitted);
    }
    public void Dispose()
    {
        _transaction?.Dispose();
        _connection?.Dispose();
    }
    private async Task<(T1Result, T2Result)> ExecuteQueryAsync<T1Result, T2Result, T>(
       string sql,
       T? param,
       CommandType? commandType,
       Func<SqlMapper.GridReader, Task<T1Result>> readFirst,
       Func<SqlMapper.GridReader, Task<T2Result>> readSecond,
       CancellationToken? cancellationToken = null
       )
    {
        CancellationToken ct = cancellationToken ?? CancellationToken.None;
        using IDbConnection db = await _IdbConnectionFactory.CreateConnection();
        using var result = await db.QueryMultipleAsync(new CommandDefinition(sql, param, commandType: commandType, cancellationToken: ct));

        T1Result item1 = await readFirst(result);
        T2Result item2 = await readSecond(result);

        return (item1, item2);
    }
}