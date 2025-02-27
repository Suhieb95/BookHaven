using BookHaven.Domain.DTOs.Books;
namespace BookHaven.Application.Interfaces.Services;
public interface IAuthorService : IGenericWriteRepository<Author, Author, int>, IGenericSpecificationReadRepository
{
    Task UpdateBookAuthors(UpdateBookAuthorsRequest request, CancellationToken? cancellationToken = default);
}