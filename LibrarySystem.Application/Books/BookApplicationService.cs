using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
using LibrarySystem.Domain.Specification.Books;

namespace LibrarySystem.Application.Books;
public class BookApplicationService(IUnitOfWork _iUnitOfWork, IFileService _fileService) : IBookApplicationService
{
    public async Task<Result<BookResponse>> GetBookById(int id, CancellationToken? cancellationToken = null)
    {
        List<BookResponse>? res = await _iUnitOfWork.Books.GetAll(new GetBookByIdSpecification(id), cancellationToken);
        if (res.FirstOrDefault() is null)
            return Result<BookResponse>.Failure(new Error("Book Doesn't Exists.", NotFound, "Not Found"));

        return Result<BookResponse>.Success(res.First());
    }
    public async Task<Result<PaginatedResponse<BookResponse>>> GetBooks(PaginationParam param, CancellationToken? cancellationToken = default)
    {
        PaginatedResponse<BookResponse>? res = await _iUnitOfWork.Books.GetPaginated(param, cancellationToken);

        if (res.Data?.Count == 0)
            return Result<PaginatedResponse<BookResponse>>.Success(res);

        SetBooksDiscountPrice(res.Data!);
        var imageTasks = res.Data!.Where(book => book.ImageUrl is not null)
                                                                                .Select(async book =>
                                                                                {
                                                                                    var images = await _fileService.GetFiles(book.ImageUrl!);
                                                                                    return (book, images);
                                                                                });

        var results = await Task.WhenAll(imageTasks);

        // Assign the fetched images to the respective books
        foreach (var (book, images) in results)
            book.ImageUrl = images!;

        return Result<PaginatedResponse<BookResponse>>.Success(res);
    }
    public async Task<Result<int>> CreateBook(CreateBookRequest request, CancellationToken? cancellationToken = null)
    {
        BookResponse? book = (await _iUnitOfWork.Books.GetAll(new GetBookByNameSpecification(request.Title), cancellationToken)).FirstOrDefault();
        if (book is not null)
            return Result<int>.Failure(new("Book with selected Title already Exists.", Conflict, "Title Exists"));

        int id = await _iUnitOfWork.Books.Add(request);

        if (request.HasImages())
        {
            FileUploadResult[] uploadResult = await _fileService.Upload(request.Images!);

            IEnumerable<Task> result = uploadResult
                .Select(x => _iUnitOfWork.Books.AddBookImagePath(new(id, x.PublicId)));

            await Task.WhenAll(result);
        }

        return Result<int>.Success(id);
    }
    private static void SetBooksDiscountPrice(IReadOnlyList<BookResponse> books)
    {
        foreach (BookResponse book in books)
            if (book.DiscountPercentage > 0)
                book.CalculateDiscountedPrice();
    }
}