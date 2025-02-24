using LibrarySystem.Domain.Specification.Genres;
namespace LibrarySystem.Application.Genres;
public class GenreApplicationService(IUnitOfWork _unitOfWork) : IGenreApplicationService
{
    public async Task<Result<int>> Add(Genre entity, CancellationToken? cancellationToken = null)
    {
        List<Genre> usedGenre = await _unitOfWork.Genres.GetAll(new GetGenreByName(entity.Name), cancellationToken);

        if (usedGenre.FirstOrDefault() is not null)
            return Result<int>.Failure(new Error("Genre already exists", Conflict, "Genre already exists"));

        int id = await _unitOfWork.Genres.Add(entity, cancellationToken);
        return Result<int>.Success(id);
    }
    public async Task<Result<bool>> Delete(int id, CancellationToken? cancellationToken = null)
    {
        List<Genre> currentGenre = await _unitOfWork.Genres.GetAll(new GetGenreById(id), cancellationToken);
        if (currentGenre.FirstOrDefault() is null)
            return Result<bool>.Failure(new Error("Genre does not exist, you can't do this action", NotFound, "Genre was not found"));

        List<Genre> usedGenre = await _unitOfWork.Genres.GetAll(new GetUsedGenre(id), cancellationToken);
        if (usedGenre.FirstOrDefault() is not null)
            return Result<bool>.Failure(new Error("Genre is in use", Conflict, "Genre is used"));


        await _unitOfWork.Genres.Delete(id, cancellationToken);

        return Result<bool>.Success(true);
    }
}