using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;

namespace LibrarySystem.Application.Books;
public class BookApplicationService(IUnitOfWork _iUnitOfWork, IFileService _fileService) : IBookApplicationService
{
    public async Task<Result<Book>> GetBookById(int id, CancellationToken? cancellationToken = null)
    {
        var res = await _iUnitOfWork.Books.GetById(id, cancellationToken);
        if (res is null)
            return Result<Book>.Failure(new Error("Book Doesn't Exists.", NotFound, "Not Found"));

        return Result<Book>.Success(res);
    }
    public async Task<Result<PaginatedResponse<BooksResponse>>> GetBooks(PaginationParam param, CancellationToken? cancellationToken = null)
    {
        PaginatedResponse<BooksResponse>? res = await _iUnitOfWork.Books.GetAll(param, cancellationToken);

        if (res.Data?.Count == 0)
            return Result<PaginatedResponse<BooksResponse>>.Success(res);

        /*
        foreach (BooksResponse book in res.Data!)
        {
            if (book.ImageUrl is null) continue;
            var images = await _fileService.GetFiles(book.ImageUrl)!;
            book.ImageUrl = images!;
        }

        */

        var imageTasks = res.Data!
       .Where(book => book.ImageUrl is not null)
       .Select(async book =>
       {
           var images = await _fileService.GetFiles(book.ImageUrl!);
           return (book, images);
       });
        var results = await Task.WhenAll(imageTasks);

        // Assign the fetched images to the respective books
        foreach (var (book, images) in results)
            book.ImageUrl = images!;


        return Result<PaginatedResponse<BooksResponse>>.Success(res);
    }
}