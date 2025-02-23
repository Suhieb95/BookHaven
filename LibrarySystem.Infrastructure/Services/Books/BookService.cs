using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Specification;
namespace LibrarySystem.Infrastructure.Services.Books;
public class BookService(ISqlDataAccess _sqlDataAccess) : IBookService
{
    public async Task<PaginatedResponse<BooksResponse>> GetAll(PaginationParam param, CancellationToken? cancellationToken = null)
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
        foreach (BooksResponse book in books)
        {
            List<string> bookImages = await _sqlDataAccess.LoadData<string>(Sql, new { book.Id });
            if (bookImages.Count == 0) continue;
            book.ImageUrl = [.. bookImages];
        }

        return response;
    }
    public async Task<Book?> GetById(int id, CancellationToken? cancellationToken = null, Specification? specification = null)
    {
        var res = await _sqlDataAccess.LoadData<Book>("SPGetBookById", new { Id = id }, StoredProcedure, cancellationToken);
        return res.FirstOrDefault();
    }
}
