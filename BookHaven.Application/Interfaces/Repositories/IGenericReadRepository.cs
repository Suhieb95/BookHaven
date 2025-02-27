using BookHaven.Domain.Specification;
namespace BookHaven.Application.Interfaces.Repositories;
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
public interface IGenericReadRepository
{
    Task<TResult?> GetBy<TResult>(Specification<TResult> param, CancellationToken? cancellationToken = default);
    Task<List<TResult>> GetAll<TResult>(Specification<TResult> param, CancellationToken? cancellationToken = default);
}