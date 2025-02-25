using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
using LibrarySystem.Domain.Specification;

namespace LibrarySystem.Application.Interfaces.Services;
public interface IBookService : IGenericReadPaginatedRepository<PaginatedResponse<BookResponse>, PaginationParam>,
IGenericReadByIdRepository<BookResponse, Specification>,
IGenericWriteRepository<CreateBookRequest, UpdateBookRequest, int>
{
    Task AddBookImagePath(CreateBookImage createBookImage, CancellationToken? cancellationToken = default);
    Task<List<T>> GetAll<T>(Specification<T> specification, CancellationToken? cancellationToken = default);
    Task DeleteBookImages(int id, string[] paths, CancellationToken? cancellationToken = default);
    Task UpdateBookImages(int id, string[] paths, CancellationToken? cancellationToken = default);
}