using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
using LibrarySystem.Domain.Specification;
using LibrarySystem.Infrastructure.Mappings.Book;
namespace LibrarySystem.Infrastructure.Services.Books;
public class BookService(ISqlDataAccess _sqlDataAccess) : IBookService
{
    public async Task<PaginatedResponse<BookResponse>> GetPaginated(PaginationParam param, CancellationToken? cancellationToken = null)
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
                book.ImageUrl = [.. bookImages];

        return response;
    }
    public async Task<List<BookResponse>> GetAll(Specification param, CancellationToken? cancellationToken = null)
       => await _sqlDataAccess.LoadData<BookResponse>(param.ToSql(), param.Parameters, param.CommandType, cancellationToken: cancellationToken);

    public async Task<int> Add(CreateBookRequest request, CancellationToken? cancellationToken = null)
    {
        const string Sql = "SPCreateBook";
        int result = await _sqlDataAccess.SaveData<int>(Sql, request.ToParameter(), StoredProcedure, cancellationToken);
        return result;
    }
    public Task Update(UpdateBookRequest entity, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }
    public async Task Delete(int id, CancellationToken? cancellationToken = null)
    {
        const string Sql = "DELETE FROM Books WHERE Id = @Id";
        await _sqlDataAccess.SaveData<int>(Sql, new { id }, cancellationToken: cancellationToken);
    }
    public async Task AddBookImagePath(CreateBookImage createBookImage, CancellationToken? cancellationToken = default)
    {
        const string Sql = "INSERT INTO BookImages (ImageUrl, BookId) VALUES (@ImageUrl, @BookId)";
        await _sqlDataAccess.SaveData(Sql, createBookImage, cancellationToken: cancellationToken);
    }
}