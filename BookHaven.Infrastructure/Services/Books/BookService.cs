using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs;
using BookHaven.Domain.DTOs.Books;
using BookHaven.Domain.Specification;
using BookHaven.Domain.Specification.Authors;
using BookHaven.Domain.Specification.Genres;
using BookHaven.Infrastructure.Mappings.Book;

namespace BookHaven.Infrastructure.Services.Books;
public class BookService(ISqlDataAccess _sqlDataAccess, IAuthorService _authorService, IGenreService _genreService, IMssqlDbTransaction _mssqlDbTransaction)
    : GenericSpecificationReadRepository(_sqlDataAccess), IBookService
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

        IEnumerable<Task>? tasks = response.Data.Select(x => GetBookAuthorsAndGenres(x, cancellationToken));
        await Task.WhenAll(tasks);

        return response;
    }
    public async Task<int> Add(CreateBookRequest request, CancellationToken? cancellationToken = default)
    {
        try
        {
            const string Sql = "SPCreateBook";
            await _mssqlDbTransaction.InitilizeTransaction();
            int result = await _mssqlDbTransaction.SaveDataInTransaction<int>(Sql, request.ToParameter(), StoredProcedure, cancellationToken);
            await _authorService.UpdateBookAuthors(new(result, request.Authors), cancellationToken);
            await _genreService.UpdateBookGenres(new(result, request.Genres), cancellationToken);
            _mssqlDbTransaction.CommitTransaction();
            return result;
        }
        catch
        {
            _mssqlDbTransaction.RollbackTransaction();
            throw;
        }
    }
    public async Task Update(UpdateBookRequest request, CancellationToken? cancellationToken = default)
    {
        try
        {
            const string Sql = "SPUpdateBook";
            await _mssqlDbTransaction.InitilizeTransaction();
            await _mssqlDbTransaction.SaveDataInTransaction(Sql, request.ToParameter(), StoredProcedure, cancellationToken);
            await _authorService.UpdateBookAuthors(new(request.Id, request.Authors), cancellationToken);
            await _genreService.UpdateBookGenres(new(request.Id, request.Genres), cancellationToken);
            _mssqlDbTransaction.CommitTransaction();
        }
        catch
        {
            _mssqlDbTransaction.RollbackTransaction();
            throw;
        }
    }
    public async Task Delete(int id, CancellationToken? cancellationToken = default)
    {
        const string Sql = "DELETE FROM Books WHERE Id = @Id";
        await _sqlDataAccess.SaveData<int>(Sql, new { id }, cancellationToken: cancellationToken);
    }
    public async Task<BookResponse> GetByIdWithDetails(Specification param, CancellationToken? cancellationToken = null)
    {
        (List<string> images, BookResponse? book) = await _sqlDataAccess.FetchListAndSingleAsync<string, BookResponse>(param.ToSql(),
                                 cancellationToken, param.Parameters, param.CommandType)!;

        if (images.Count > 0)
            book!.ImageUrls = [.. images];

        await GetBookAuthorsAndGenres(book!, cancellationToken);
        return book!;
    }
    private async Task GetBookAuthorsAndGenres(BookResponse book, CancellationToken? cancellationToken = default)
    {
        book.Authors = await _authorService.GetAll(new GetAuthorsByBookId(book.Id), cancellationToken);
        book.Genres = await _genreService.GetAll(new GetGenresByBookId(book.Id), cancellationToken);
    }
}