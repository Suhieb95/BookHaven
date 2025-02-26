using BookHaven.Domain.DTOs.Books;
using BookHaven.Domain.Specification;

namespace BookHaven.Application.Interfaces.Services;
public interface IAuthorService : IGenericWriteRepository<Author, Author, int>
{
    Task<List<T>> GetAll<T>(Specification<T> specification, CancellationToken? cancellationToken = default);
    Task UpdateBookAuthors(UpdateBookAuthorsRequest request, CancellationToken? cancellationToken = default);
}