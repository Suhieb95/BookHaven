using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
namespace LibrarySystem.Application.Books;
public interface IBookApplicationService
{
    public Task<Result<PaginatedResponse<BookResponse>>> GetBooks(PaginationParam param, CancellationToken? cancellationToken = null);
    public Task<Result<BookResponse>> GetBookById(int id, CancellationToken? cancellationToken = null);
}