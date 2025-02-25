using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
namespace LibrarySystem.Application.Books;
public interface IBookApplicationService
{
    Task<Result<PaginatedResponse<BookResponse>>> GetBooks(PaginationParam param, CancellationToken? cancellationToken = default);
    Task<Result<BookResponse>> GetBookById(int id, CancellationToken? cancellationToken = default);
    Task<Result<int>> CreateBook(CreateBookRequest request, CancellationToken? cancellationToken = default);
}