namespace InventoryManagement.Application.Interfaces.Repositories;

public interface IGenericReadRepository<T, U> where T : class
{
    Task<T?> GetByIdAsync(U id);
    Task<List<T>> GetAllAsync();
}
public interface IGenericReadWithParamRepository<T, P> where T : class
{
    Task<T> GetAllAsync(P param);
}
public interface IGenericReadByIdRepository<T, U> where T : class?
{
    Task<T> GetByIdAsync(U id);
}
