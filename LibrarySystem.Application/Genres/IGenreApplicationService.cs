namespace LibrarySystem.Application.Genres;
public interface IGenreApplicationService
{
    Task<Result<int>> Add(Genre entity, CancellationToken? cancellationToken = null);
}
