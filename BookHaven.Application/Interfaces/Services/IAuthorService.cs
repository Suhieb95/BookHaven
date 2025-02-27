using BookHaven.Domain.DTOs.Books;
using BookHaven.Domain.Specification;

namespace BookHaven.Application.Interfaces.Services;
public interface IAuthorService : IGenericWriteRepository<Author, Author, int>, IGenericSpecificationReadRepository
{
    Task UpdateBookAuthors(UpdateBookAuthorsRequest request, CancellationToken? cancellationToken = default);
}