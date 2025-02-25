using LibrarySystem.Domain.Specification;

namespace LibrarySystem.Application.Interfaces.Services;
public interface IAuthorService
{
    Task<List<T>> GetAll<T>(Specification<T> specification, CancellationToken? cancellationToken = default);
}