using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Specification.Books;

namespace LibrarySystem.Application.Books;
public class BookApplicationService(IBookService _bookService) : IBookApplicationService
{
    public async Task<Result<Book>> GetBookById(int id, CancellationToken? cancellationToken = null)
    {
        var res = await _bookService.GetById(id, cancellationToken);
        if (res is null)
            return Result<Book>.Failure(new Error("Book Doesn't Exists.", HttpStatusCode.NotFound, "Not Found"));

        return Result<Book>.Success(res);
    }
    public async Task<Result<PaginatedResponse<Book>>> GetBooks(PaginationParam param, CancellationToken? cancellationToken = null)
    {
        PaginatedResponse<Book>? res = await _bookService.GetAll(param, cancellationToken);
        return Result<PaginatedResponse<Book>>.Success(res);
    }
}

