using LibrarySystem.Domain.DTOs;

namespace LibrarySystem.Application.Genres;
public interface IGenreApplicationService
{
    Task<Result<int>> Add(Genre entity, CancellationToken? cancellationToken = null);
    Task<Result<bool>> Delete(int id, CancellationToken? cancellationToken = null);
    Task<Result<bool>> Update(Genre genre, CancellationToken? cancellationToken = null);
    Task<Result<PaginatedResponse<Genre>>> GetPaginatedGenres(PaginationParam param, CancellationToken? cancellationToken = null);
}