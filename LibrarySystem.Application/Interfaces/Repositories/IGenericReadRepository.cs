using LibrarySystem.Domain.Specification;

namespace LibrarySystem.Application.Interfaces.Repositories;

public interface IGenericReadRepository<T, U> where T : class
{
    Task<T?> GetById(U id, CancellationToken? cancellationToken = null, Specification? specification = null);
    Task<List<T>> GetAll(CancellationToken? cancellationToken = null, Specification? specification = null);
}
public interface IGenericReadWithParamRepository<T> where T : class
{
    Task<T> GetAll(Specification? specification = null, CancellationToken? cancellationToken = null);
}
public interface IGenericReadByIdRepository<T, U> where T : class?
{
    Task<T> GetById(U id, CancellationToken? cancellationToken = null, Specification? specification = null);
}
