using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Specification;

namespace LibrarySystem.Infrastructure.Services.Genres;
public class GenreService(ISqlDataAccess sqlDataAccess) : IGenreService
{
    public async Task<int> Add(Genre entity, CancellationToken? cancellationToken = null)
        => await sqlDataAccess.SaveData<int>("SPCreateGenre", new { entity.Name }, StoredProcedure, cancellationToken);
    public async Task Delete(int id, CancellationToken? cancellationToken = null)
        => await sqlDataAccess.SaveData("Delete from Genres where Id = @Id", new { Id = id }, cancellationToken: cancellationToken);
    public async Task Update(Genre entity, CancellationToken? cancellationToken = null)
        => await sqlDataAccess.SaveData("Update Genres set Name = @Name where Id = @Id", entity, cancellationToken: cancellationToken);
    public async Task<List<Genre>> GetAll(Specification param, CancellationToken? cancellationToken = null)
        => await sqlDataAccess.LoadData<Genre>(param.ToSql(), param.Parameters, cancellationToken: cancellationToken);
    public async Task<PaginatedResponse<Genre>> GetPaginated(PaginationParam param, CancellationToken? cancellationToken = null)
    {
        const string Sql = "SPGetGenres";
        PaginatedResponse<Genre> response = new();
        (List<Genre>? genres, PaginationDetails? paginationDetails) = await sqlDataAccess.FetchListAndSingleAsync<Genre, PaginationDetails>(Sql, cancellationToken, param, StoredProcedure);

        response.Data = genres;
        response.PaginationDetails = paginationDetails!;
        response.SetTotalPage(param.PageSize);

        return response;
    }
}