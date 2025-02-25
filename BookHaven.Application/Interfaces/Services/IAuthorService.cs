using BookHaven.Domain.Specification;

namespace BookHaven.Application.Interfaces.Services;
public interface IAuthorService
{
    Task<List<T>> GetAll<T>(Specification<T> specification, CancellationToken? cancellationToken = default);
}