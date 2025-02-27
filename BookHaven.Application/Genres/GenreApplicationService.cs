using BookHaven.Domain.DTOs;
using BookHaven.Domain.Specification.Genres;
namespace BookHaven.Application.Genres;
public class GenreApplicationService(IUnitOfWork _unitOfWork) : IGenreApplicationService
{
    public async Task<Result<int>> Add(Genre entity, CancellationToken? cancellationToken = null)
    {
        Genre? usedGenre = await _unitOfWork.Genres.GetBy(new GetGenreByName(entity.Name), cancellationToken);

        if (usedGenre is not null)
            return Result<int>.Failure(new Error("Genre already exists", Conflict, "Genre already exists"));

        int id = await _unitOfWork.Genres.Add(entity, cancellationToken);
        return Result<int>.Success(id);
    }
    public async Task<Result<bool>> Delete(int id, CancellationToken? cancellationToken = null)
    {
        Genre? currentGenre = await _unitOfWork.Genres.GetBy(new GetGenreById(id), cancellationToken);
        if (currentGenre is null)
            return Result<bool>.Failure(new Error("Genre does not exist, you can't do this action", NotFound, "Genre was not found"));

        bool isGenreUsed = await _unitOfWork.Genres.GetBy(new GetUsedGenre(id), cancellationToken);
        if (isGenreUsed)
            return Result<bool>.Failure(new Error("Genre is in use", Conflict, "Genre is used"));

        await _unitOfWork.Genres.Delete(id, cancellationToken);
        return Result<bool>.Success(true);
    }
    public async Task<Result<Genre>> GetById(int id, CancellationToken? cancellationToken = null)
    {
        Genre? currentGenre = await _unitOfWork.Genres.GetBy(new GetGenreById(id), cancellationToken);
        return currentGenre is null
            ? Result<Genre>.Failure(new Error("Genre does not exist, you can't do this action", NotFound, "Genre was not found"))
            : Result<Genre>.Success(currentGenre);
    }
    public async Task<Result<PaginatedResponse<Genre>>> GetPaginatedGenres(PaginationParam param, CancellationToken? cancellationToken = null)
    {
        PaginatedResponse<Genre>? result = await _unitOfWork.Genres.GetPaginated(param, cancellationToken);
        return Result<PaginatedResponse<Genre>>.Success(result);
    }
    public async Task<Result<bool>> Update(Genre genre, CancellationToken? cancellationToken = null)
    {
        Genre? currentGenre = await _unitOfWork.Genres.GetBy(new GetGenreById(genre.Id), cancellationToken);
        if (currentGenre is null)
            return Result<bool>.Failure(new Error("Genre does not exist.", NotFound, "Genre not found"));

        Genre? usedGenre = await _unitOfWork.Genres.GetBy(new GetGenreByName(genre.Name, genre.Id), cancellationToken);
        if (usedGenre is not null)
            return Result<bool>.Failure(new Error("Genre already exists", Conflict, "Genre exists"));

        await _unitOfWork.Genres.Update(genre, cancellationToken);
        return Result<bool>.Success(true);
    }
}