namespace InventoryManagement.Application.Interfaces.Repositories;

public interface IGenericWriteRepository<T, U, P> where T : class
{
    Task<P?> AddAsync(T entity);
    Task UpdateAsync(U entity);
    Task DeleteAsync(P id);
}
public interface IGenericWriteRepositoryWithResponse<T, TResponse, TId> where T : class
{
    Task<TResponse?> AddAsync(T entity);
    Task DeleteAsync(TId id);
}