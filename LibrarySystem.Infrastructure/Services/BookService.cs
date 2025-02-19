using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Specification;
namespace LibrarySystem.Infrastructure.Services;
public class BookService(ISqlDataAccess _sqlDataAccess) : IBookService
{
    public async Task<PaginatedResponse<Book>> GetAll(PaginationParam param, CancellationToken? cancellationToken = null, Specification? specification = null)
    {
        (List<Book> books, PaginationDetails? paginationDetails) = await _sqlDataAccess.FetchListAndSingleAsync<Book, PaginationDetails>
             ("SPGetBooks", cancellationToken, param, CommandType.StoredProcedure);

        PaginatedResponse<Book> response = new()
        {
            Data = books,
            PaginationDetails = paginationDetails!
        };

        response.SetTotalPage(param.PageSize);
        return response;
    }
    public async Task<Book?> GetById(int id, CancellationToken? cancellationToken = null, Specification? specification = null)
    {
        var res = await _sqlDataAccess.LoadData<Book>("SPGetBookById", new { Id = id }, CommandType.StoredProcedure, cancellationToken);
        return res.FirstOrDefault();
    }
}
