using BookHaven.Application.Helpers;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs;
using BookHaven.Domain.DTOs.Books;
using BookHaven.Domain.Specification.Books;

using static BookHaven.Application.Helpers.Extensions;

namespace BookHaven.Application.Books;
public class BookApplicationService(IUnitOfWork _iUnitOfWork, IFileService _fileService) : IBookApplicationService
{
    public async Task<Result<BookResponse>> GetBookById(int id, CancellationToken? cancellationToken = null)
    {
        BookResponse? res = await GetById(id, cancellationToken);
        if (res is null)
            return Result<BookResponse>.Failure(new Error("Book Doesn't Exists.", NotFound, "Not Found"));

        BookResponse? book = await _iUnitOfWork.Books.GetByIdWithDetails(new GetBookByIdWithImages(id), cancellationToken);
        book.CalculateDiscountedPrice();

        if (!book.ImageUrls.IsEmpty())
        {
            string?[]? imagesPaths = await _fileService.GetFiles(book.ImageUrls!);
            book.ImageUrls = imagesPaths!;
        }

        return Result<BookResponse>.Success(book);
    }
    public async Task<Result<PaginatedResponse<BookResponse>>> GetBooks(PaginationParam param, CancellationToken? cancellationToken = default)
    {
        PaginatedResponse<BookResponse>? res = await _iUnitOfWork.Books.GetPaginated(param, cancellationToken);

        if (res.Data?.Count == 0)
            return Result<PaginatedResponse<BookResponse>>.Success(res);

        SetBooksDiscountedPrice(res.Data!);
        var imageTasks = res.Data!.Where(book => book.ImageUrls is not null)
                                                                                .Select(async book =>
                                                                                {
                                                                                    var images = await _fileService.GetFiles(book.ImageUrls!);
                                                                                    return (book, images);
                                                                                });

        var results = await Task.WhenAll(imageTasks);

        // Assign the fetched images to the respective books
        foreach (var (book, images) in results)
            book.ImageUrls = images!;

        return Result<PaginatedResponse<BookResponse>>.Success(res);
    }
    public async Task<Result<int>> CreateBook(CreateBookRequest request, CancellationToken? cancellationToken = null)
    {
        (bool flowControl, Result<int>? value) = await ValidateBook<int>(request.Title, request.ISBN, cancellationToken: cancellationToken);
        if (flowControl == false)
            return value!;

        int id = await _iUnitOfWork.Books.Add(request);

        if (request.Images.HasImages())
        {
            FileUploadResult[] uploadResult = await _fileService.Upload(request.Images!);

            IEnumerable<Task> result = uploadResult
                .Select(x => _iUnitOfWork.BookImages.Add(new(id, x.PublicId)));

            await Task.WhenAll(result);
        }

        return Result<int>.Success(id);
    }
    public async Task<Result<bool>> DeleteBook(int id, CancellationToken? cancellationToken = null)
    {
        BookResponse? res = await GetById(id, cancellationToken);
        if (res is null)
            return Result<bool>.Failure(new Error("Book Doesn't Exists.", NotFound, "Not Found"));

        bool isUsed = await _iUnitOfWork.Books.GetBy(new GetBookUsed(id), cancellationToken);
        if (isUsed)
            return Result<bool>.Failure(new("Selected Book Cannot Be deleted, because it's being used.", BadRequest, "Book In use"));

        await _iUnitOfWork.Books.Delete(id, cancellationToken);
        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> UpdateBook(UpdateBookRequest request, CancellationToken? cancellationToken = null)
    {
        BookResponse? res = await GetById(request.Id, cancellationToken);
        if (res is null)
            return Result<bool>.Failure(new Error("Book Doesn't Exists.", NotFound, "Not Found"));

        (bool flowControl, Result<bool>? value) = await ValidateBook<bool>(request.Title, request.ISBN, request.Id, cancellationToken);
        if (flowControl == false)
            return value!;

        await _iUnitOfWork.Books.Update(request, cancellationToken);
        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> DeleteBookImages(DeleteBookImageRequest request, CancellationToken? cancellationToken = null)
    {
        BookResponse? res = await GetById(request.Id, cancellationToken);
        if (res is null)
            return Result<bool>.Failure(new Error("Book Doesn't Exists.", NotFound, "Not Found"));

        if (request.Paths.IsEmpty())
            return Result<bool>.Failure(new Error("Not Images were Selected", BadRequest, "Empty Paths"));

        string[] publicIds = GetPublicIds(request.Paths);
        await _iUnitOfWork.BookImages.Delete(new UpdateBookImagesResult(request.Id, publicIds), cancellationToken);

        IEnumerable<Task<bool>>? imagesTasks = publicIds.Select(_fileService.Delete);
        await Task.WhenAll(imagesTasks);

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> UpdateBookImages(UpdateBookImageRequest request, CancellationToken? cancellationToken = null)
    {
        BookResponse? res = await GetById(request.Id, cancellationToken);
        if (res is null)
            return Result<bool>.Failure(new Error("Book Doesn't Exists.", NotFound, "Not Found"));

        if (!request.Images.HasImages())
            return Result<bool>.Failure(new Error("No Images were Selected", BadRequest, "Empty Paths"));

        FileUploadResult[]? uploadResults = await _fileService.Upload(request.Images);
        string[] paths = [.. uploadResults.Select(x => x.PublicId)];
        await _iUnitOfWork.BookImages.Update(new UpdateBookImagesResult(request.Id, paths), cancellationToken);

        return Result<bool>.Success(true);
    }
    private static string[] GetPublicIds(string[] paths)
        => paths.Select(x =>
            {
                string[] parts = x.Split("/");
                string publicId = parts[^1]; // Last Part after the last "/".
                return publicId.Split('.')[0]; // Extract the public ID.
            }).ToArray();
    private async Task<BookResponse?> GetById(int id, CancellationToken? cancellationToken = default)
        => await _iUnitOfWork.Books.GetBy(new GetBookByIdSpecification(id), cancellationToken);
    private static void SetBooksDiscountedPrice(IReadOnlyList<BookResponse> books)
    {
        foreach (BookResponse book in books)
            book.CalculateDiscountedPrice();
    }
    private async Task<(bool flowControl, Result<T>? value)> ValidateBook<T>(string title, string isbn, int? id = null, CancellationToken? cancellationToken = default)
    {
        bool isBookNameFound = await _iUnitOfWork.Books.GetBy(new IsBookTitleUniqueSpecification(title, id), cancellationToken);
        if (isBookNameFound)
            return (flowControl: false, value: Result<T>.Failure(new("Book with selected Title already Exists.", Conflict, "Title Exists")));

        bool isBookISBNFound = await _iUnitOfWork.Books.GetBy(new IsBookISBNUniqueSpecification(isbn, id), cancellationToken);
        if (isBookISBNFound)
            return (flowControl: false, value: Result<T>.Failure(new("Book with selected ISBN already Exists.", Conflict, "ISBN Exists")));

        return (flowControl: true, value: null);
    }
}