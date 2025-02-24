using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
using LibrarySystem.Domain.Specification;
namespace LibrarySystem.Infrastructure.Services.Books;
public class BookService(ISqlDataAccess _sqlDataAccess) : IBookService
{
    public async Task<PaginatedResponse<BooksResponse>> GetPaginated(PaginationParam param, CancellationToken? cancellationToken = null)
    {
        (List<BooksResponse> books, PaginationDetails? paginationDetails) = await _sqlDataAccess.FetchListAndSingleAsync<BooksResponse, PaginationDetails>
                     ("SPGetBooks", cancellationToken, param, StoredProcedure);

        PaginatedResponse<BooksResponse> response = new()
        {
            Data = books,
            PaginationDetails = paginationDetails!
        };

        response.SetTotalPage(param.PageSize);

        const string Sql = "SELECT ImageUrl FROM BookImages WHERE BookId = @Id";
        /*   foreach (BooksResponse book in books)
         {
             List<string> bookImages = await _sqlDataAccess.LoadData<string>(Sql, new { book.Id });
             if (bookImages.Count == 0) continue;
             book.ImageUrl = [.. bookImages];
         } */

        var imageTasks = books.Select(async book =>
        {
            List<string> bookImages = await _sqlDataAccess.LoadData<string>(Sql, new { book.Id });
            return (book, bookImages);
        });

        (BooksResponse book, List<string> bookImages)[]? results = await Task.WhenAll(imageTasks);

        // Assign the retrieved images to each book
        foreach (var (book, bookImages) in results)
            if (bookImages.Count > 0)
                book.ImageUrl = [.. bookImages];

        return response;
    }
    public async Task<List<BooksResponse>> GetAll(Specification param, CancellationToken? cancellationToken = null)
       => await _sqlDataAccess.LoadData<BooksResponse>(param.ToSql(), param.Parameters, StoredProcedure, cancellationToken);
}