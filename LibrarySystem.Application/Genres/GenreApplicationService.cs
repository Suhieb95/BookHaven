namespace LibrarySystem.Application.Genres;
public class GenreApplicationService(IUnitOfWork unitOfWork) : IGenreApplicationService
{
    public async Task<Result<int>> Add(Genre entity, CancellationToken? cancellationToken = null)
    {
        /*
         need to check if genre name is in use
        */

        int genre = await unitOfWork.Genres.Add(entity, cancellationToken);
        return Result<int>.Success(genre);
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken? cancellationToken = null)
    {
        /*
        need to check if genre id exists 
       */
        await unitOfWork.Genres.Delete(id, cancellationToken);
        return Result<bool>.Success(true);
    }
}
