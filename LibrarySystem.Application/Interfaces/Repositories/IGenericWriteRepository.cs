namespace LibrarySystem.Application.Interfaces.Repositories;

public interface IGenericWriteRepository<T, U, P> where T : class
{
    Task<P?> Add(T entity, CancellationToken? cancellationToken = null);
    Task Update(U entity, CancellationToken? cancellationToken = null);
    Task Delete(P id, CancellationToken? cancellationToken = null);
}
public interface IGenericWriteRepositoryWithResponse<T, TResponse, TId> where T : class
{
    Task<TResponse?> Add(T entity, CancellationToken? cancellationToken = null);
    Task Delete(TId id, CancellationToken? cancellationToken = null);
}