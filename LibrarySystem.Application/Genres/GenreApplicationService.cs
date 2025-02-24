using LibrarySystem.Domain.DTOs;
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
    public async Task<Result<PaginatedResponse<Genre>>> GetPaginatedGenres(PaginationParam param, CancellationToken? cancellationToken = null)
    {
        PaginatedResponse<Genre>? result = await _unitOfWork.Genres.GetPaginated(param, cancellationToken);
        return Result<PaginatedResponse<Genre>>.Success(result);
    }
    public async Task<Result<bool>> Update(Genre genre, CancellationToken? cancellationToken = null)
    {
        List<Genre> currentGenre = await _unitOfWork.Genres.GetAll(new GetGenreById(genre.Id), cancellationToken);
        if (currentGenre.FirstOrDefault() is null)
            return Result<bool>.Failure(new Error("Genre does not exist.", NotFound, "Genre not found"));

        List<Genre> usedGenre = await _unitOfWork.Genres.GetAll(new GetGenreByName(genre.Name), cancellationToken);
        if (usedGenre.FirstOrDefault() is not null)
            return Result<bool>.Failure(new Error("Genre already exists", Conflict, "Genre exists"));

        await _unitOfWork.Genres.Update(genre, cancellationToken);
        return Result<bool>.Success(true);
    }
}