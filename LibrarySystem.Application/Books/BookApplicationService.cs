using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.Books;
public class BookApplicationService(IBookService _bookService) : IBookApplicationService
{
    public async Task<Result<Book>> GetBookById(int id, CancellationToken? cancellationToken = null)
    {
        var res = await _bookService.GetById(id, cancellationToken);
        if (res is null)
            return Result<Book>.Failure(new Error("Book Doesn't Exists.", NotFound, "Not Found"));

        return Result<Book>.Success(res);
    }
    public async Task<Result<PaginatedResponse<BookResponse>>> GetBooks(PaginationParam param, CancellationToken? cancellationToken = null)
    {
        PaginatedResponse<BookResponse>? res = await _bookService.GetAll(param, cancellationToken);
        return Result<PaginatedResponse<BookResponse>>.Success(res);
    }
}

