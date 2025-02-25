namespace LibrarySystem.Application.Interfaces.Repositories;

public interface IGenericWriteRepository<T, U, P> where T : class
{
    Task<P?> Add(T entity, CancellationToken? cancellationToken = default);
    Task Update(U entity, CancellationToken? cancellationToken = default);
    Task Delete(P id, CancellationToken? cancellationToken = default);
}
public interface IGenericWriteRepositoryWithResponse<T, TResponse, TId> where T : class
{
    Task<TResponse?> Add(T entity, CancellationToken? cancellationToken = default);
    Task Delete(TId id, CancellationToken? cancellationToken = default);
}