using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.Books;
public class BookApplicationService(IUnitOfWork _iUnitOfWork, IFileService _fileService) : IBookApplicationService
{
    public async Task<Result<Book>> GetBookById(int id, CancellationToken? cancellationToken = null)
    {
        var res = await _iUnitOfWork.BookService.GetById(id, cancellationToken);
        if (res is null)
            return Result<Book>.Failure(new Error("Book Doesn't Exists.", NotFound, "Not Found"));

        return Result<Book>.Success(res);
    }
    public async Task<Result<PaginatedResponse<BookResponse>>> GetBooks(PaginationParam param, CancellationToken? cancellationToken = null)
    {
        PaginatedResponse<BookResponse>? res = await _iUnitOfWork.BookService.GetAll(param, cancellationToken);

        if (res.Data?.Count == 0)
            return Result<PaginatedResponse<BookResponse>>.Success(res);

        foreach (BookResponse book in res.Data!)
        {
            if (book.ImageUrl is null) continue;
            var images = await _fileService.GetFiles(book.ImageUrl)!;
            book.ImageUrl = images!;
        }

        return Result<PaginatedResponse<BookResponse>>.Success(res);
    }
}