using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Infrastructure.Services.Genres;
public class GenreService(ISqlDataAccess sqlDataAccess) : IGenreService
{
    public async Task<int> Add(Genre entity, CancellationToken? cancellationToken = null)
        => await sqlDataAccess.SaveData<int>("SPCreateGenre", new { entity.Name }, StoredProcedure, cancellationToken);

    public async Task Delete(int id, CancellationToken? cancellationToken = null)
    => await sqlDataAccess.SaveData("Delete from Genres where Id = @Id", new { Id = id }, cancellationToken: cancellationToken);

    public async Task Update(Genre entity, CancellationToken? cancellationToken = null)
    => await sqlDataAccess.SaveData("Update Genres set Name = @Name where Id = @Id", entity, cancellationToken: cancellationToken);
}
