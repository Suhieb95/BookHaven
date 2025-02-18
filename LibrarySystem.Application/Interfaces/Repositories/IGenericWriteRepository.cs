namespace LibrarySystem.Application.Interfaces.Repositories;

public interface IGenericWriteRepository<T, U, P> where T : class
{
    Task<P?> Add(T entity);
    Task Update(U entity);
    Task Delete(P id);
}
public interface IGenericWriteRepositoryWithResponse<T, TResponse, TId> where T : class
{
    Task<TResponse?> Add(T entity);
    Task Delete(TId id);
}