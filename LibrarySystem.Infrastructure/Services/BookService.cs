using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Specification;
namespace LibrarySystem.Infrastructure.Services;
public class BookService(ISqlDataAccess _sqlDataAccess) : IBookService
{
    public async Task<PaginatedResponse<Book>> GetAll(Specification? specification = null, CancellationToken? cancellationToken = null)
    {
        var p = specification?.Parameters;
        var result = await _sqlDataAccess.FetchListAndSingleAsync<Book, PaginationDetails>
             (specification?.ToSql(), cancellationToken, p, CommandType.StoredProcedure);

        PaginatedResponse<Book> response = new()
        {
            Data = result.Item1,
            PaginationDetails = result.Item2
        };

        response.SetTotalPage(p!.PageSize, result.Item2.NoOfRecords);
        return response;
    }
    public async Task<Book?> GetById(int id, CancellationToken? cancellationToken = null, Specification? specification = null)
    {
        var res = await _sqlDataAccess.LoadData<Book>("SPGetBookById", new { Id = id }, CommandType.StoredProcedure, cancellationToken);
        return res.FirstOrDefault();
    }
}
