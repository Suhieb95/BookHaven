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
    Task<T> GetByIdWithDetails(U param, CancellationToken? cancellationToken = default);
}
public interface IGenericSpecificationReadRepository
{
    Task<List<TResult>> GetAll<TResult>(Specification<TResult> param, CancellationToken? cancellationToken = null);
    Task<TResult?> GetBy<TResult>(Specification<TResult> param, CancellationToken? cancellationToken = null);
}