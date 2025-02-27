using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs;
using BookHaven.Domain.DTOs.Books;
using BookHaven.Domain.Entities;

namespace BookHaven.Infrastructure.Services.Genres;
public class GenreService(ISqlDataAccess _sqlDataAccess, IMssqlDbTransaction _mssqlDbTransaction) : GenericSpecificationReadRepository(_sqlDataAccess), IGenreService
{
    public async Task<int> Add(Genre entity, CancellationToken? cancellationToken = null)
        => await _sqlDataAccess.SaveData<int>("SPCreateGenre", new { entity.Name }, StoredProcedure, cancellationToken);
    public async Task Delete(int id, CancellationToken? cancellationToken = null)
        => await _sqlDataAccess.SaveData("Delete from Genres where Id = @Id", new { Id = id }, cancellationToken: cancellationToken);
    public async Task Update(Genre entity, CancellationToken? cancellationToken = null)
        => await _sqlDataAccess.SaveData("Update Genres set Name = @Name where Id = @Id", entity, cancellationToken: cancellationToken);
    public async Task<PaginatedResponse<Genre>> GetPaginated(PaginationParam param, CancellationToken? cancellationToken = null)
    {
        const string Sql = "SPGetGenres";
        PaginatedResponse<Genre> response = new();
        (List<Genre>? genres, PaginationDetails? paginationDetails) = await _sqlDataAccess.FetchListAndSingleAsync<Genre, PaginationDetails>(Sql, cancellationToken, param, StoredProcedure);

        response.Data = genres;
        response.PaginationDetails = paginationDetails!;
        response.SetTotalPage(param.PageSize);

        return response;
    }
    public async Task UpdateBookGenres(UpdateBookGenresRequest request, CancellationToken? cancellationToken = null)
    {
        const string DeleteOldSql = "DELETE FROM BookGenres WHERE BookId = @BookId AND GenreId NOT IN (@GenreIds)";
        await _mssqlDbTransaction.SaveDataInTransaction(DeleteOldSql, new { request.BookId, GenreIds = request.Genres }, cancellationToken: cancellationToken);

        const string Sql = "SPUpdateBookGenre";
        var tasks = request.Genres
                                            .Select(x => _mssqlDbTransaction.SaveDataInTransaction(Sql, new { BookId = request.BookId, GenreId = x }, StoredProcedure, cancellationToken));
        await Task.WhenAll(tasks);
    }
}