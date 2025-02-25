using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
using LibrarySystem.Domain.Specification;
using LibrarySystem.Infrastructure.Mappings.Book;
namespace LibrarySystem.Infrastructure.Services.Books;
public class BookService(ISqlDataAccess _sqlDataAccess) : IBookService
{
    public async Task<PaginatedResponse<BookResponse>> GetPaginated(PaginationParam param, CancellationToken? cancellationToken = default)
    {
        (List<BookResponse> books, PaginationDetails? paginationDetails) = await _sqlDataAccess.FetchListAndSingleAsync<BookResponse, PaginationDetails>
                     ("SPGetBooks", cancellationToken, param, StoredProcedure);

        PaginatedResponse<BookResponse> response = new()
        {
            Data = books,
            PaginationDetails = paginationDetails!
        };

        response.SetTotalPage(param.PageSize);
        const string Sql = "SELECT ImageUrl FROM BookImages WHERE BookId = @Id";

        var imageTasks = books.Select(async book =>
        {
            List<string> bookImages = await _sqlDataAccess.LoadData<string>(Sql, new { book.Id });
            return (book, bookImages);
        });

        (BookResponse book, List<string> bookImages)[]? results = await Task.WhenAll(imageTasks);

        // Assign the retrieved images to each book
        foreach (var (book, bookImages) in results)
            if (bookImages.Count > 0)
                book.ImageUrls = [.. bookImages];

        return response;
    }
    public async Task<List<T>> GetAll<T>(Specification<T> param, CancellationToken? cancellationToken = default)
       => await _sqlDataAccess.LoadData(param, cancellationToken: cancellationToken);

    public async Task<int> Add(CreateBookRequest request, CancellationToken? cancellationToken = default)
    {
        const string Sql = "SPCreateBook";
        int result = await _sqlDataAccess.SaveData<int>(Sql, request.ToParameter(), StoredProcedure, cancellationToken);
        return result;
    }
    public async Task Update(UpdateBookRequest entity, CancellationToken? cancellationToken = default)
    {
        const string Sql = "SPUpdateBook";
        await _sqlDataAccess.SaveData(Sql, entity, StoredProcedure, cancellationToken);
    }
    public async Task Delete(int id, CancellationToken? cancellationToken = default)
    {
        const string Sql = "DELETE FROM Books WHERE Id = @Id";
        await _sqlDataAccess.SaveData<int>(Sql, new { id }, cancellationToken: cancellationToken);
    }
    public async Task AddBookImagePath(CreateBookImage createBookImage, CancellationToken? cancellationToken = default)
    {
        const string Sql = "INSERT INTO BookImages (ImageUrl, BookId) VALUES (@ImageUrl, @BookId)";
        await _sqlDataAccess.SaveData(Sql, createBookImage, cancellationToken: cancellationToken);
    }
    public async Task<BookResponse> GetById(Specification param, CancellationToken? cancellationToken = null)
    {
        (List<string> images, BookResponse? book) = await _sqlDataAccess.FetchListAndSingleAsync<string, BookResponse>(param.ToSql(),
                                 cancellationToken, param.Parameters, param.CommandType)!;

        if (images.Count > 0)
            book!.ImageUrls = [.. images];

        return book!;
    }
    public async Task DeleteBookImages(int id, string[] paths, CancellationToken? cancellationToken = default)
    {
        IEnumerable<Task>? tasks = paths.Select(p => DeleteBookImage(id, p, cancellationToken));
        await Task.WhenAll(tasks);
    }
    public async Task UpdateBookImages(int id, string[] paths, CancellationToken? cancellationToken = default)
    {
        IEnumerable<Task>? tasks = paths.Select(p => UpdateBookImage(id, p, cancellationToken));
        await Task.WhenAll(tasks);
    }
    private async Task DeleteBookImage(int id, string path, CancellationToken? cancellationToken = default)
    {
        const string Sql = "DELETE FROM BookImages WHERE BookId = @BookId AND ImageUrl = @ImageUrl";
        await _sqlDataAccess.SaveData(Sql, new { BookId = id, ImageUrl = path }, cancellationToken: cancellationToken);
    }
    private async Task UpdateBookImage(int id, string path, CancellationToken? cancellationToken = default)
    {
        const string Sql = "INSERT INTO BookImages (BookId, ImageUrl) VALUES (@BookId, @ImageUrl)";
        await _sqlDataAccess.SaveData(Sql, new { BookId = id, ImageUrl = path }, cancellationToken: cancellationToken);
    }
}