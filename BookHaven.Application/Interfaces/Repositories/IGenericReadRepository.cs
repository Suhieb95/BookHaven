using BookHaven.Domain.Specification;
namespace BookHaven.Application.Interfaces.Repositories;
public interface IGenericReadRepository<T, U> where T : class
{
    Task<T?> GetById(U param, CancellationToken? cancellationToken = default);
    Task<List<T>> GetAll(Specification? specification = null, CancellationToken? cancellationToken = default);
}
public interface IGenericReadWithParamRepository<T, P> where T : class
{
    Task<T> GetAll(P param, CancellationToken? cancellationToken = default);
}
public interface IGenericReadPaginatedRepository<T, P> where T : class
{
    Task<T> GetPaginated(P param, CancellationToken? cancellationToken = default);
}
public interface IGenericReadByIdRepository<T, U> where T : class?
{
    Task<T> GetById(U param, CancellationToken? cancellationToken = default);
}