using BookHaven.Domain.Specification.Authors;
namespace BookHaven.Application.Authors;
public class AuthorApplicationService(IUnitOfWork _unitOfWork) : IAuthorApplicationService
{
    public async Task<Result<int>> Create(Author request, CancellationToken? cancellationToken = null)
    {
        int result = await _unitOfWork.Authors.Add(request, cancellationToken);
        return Result<int>.Success(result);
    }
    public async Task<Result<bool>> Delete(int id, CancellationToken? cancellationToken = null)
    {
        Author? author = await GetAuthorById(id, cancellationToken);
        if (author is null)
            return Result<bool>.Failure(new Error("Author Doesn't Exists", NotFound, "Author Not Found"));

        bool isAuthorUsed = await _unitOfWork.Authors.GetBy(new GetUsedAuthor(id), cancellationToken);
        if (isAuthorUsed)
            return Result<bool>.Failure(new Error("Author is in use", Conflict, "Author is used"));

        await _unitOfWork.Authors.Delete(id, cancellationToken);
        return Result<bool>.Success(true);
    }
    public async Task<Result<List<Author>>> GetAll(CancellationToken? cancellationToken = null)
      => Result<List<Author>>.Success(await _unitOfWork.Authors.GetAll(new GetAuthors(), cancellationToken));

    public async Task<Result<Author>> GetById(int id, CancellationToken? cancellationToken = null)
    {
        Author? author = await GetAuthorById(id, cancellationToken);
        return author is null
            ? Result<Author>.Failure(new Error("Author Doesn't Exists", NotFound, "Author Not Found"))
            : Result<Author>.Success(author);
    }
    public async Task<Result<bool>> Update(Author request, CancellationToken? cancellationToken = null)
    {
        Author? author = await GetAuthorById(request.Id, cancellationToken);
        if (author is null)
            return Result<bool>.Failure(new Error("Author Doesn't Exists", NotFound, "Author Not Found"));

        bool isAuthorNameFound = await _unitOfWork.Authors.GetBy(new IsAuthorNameUnique(request.Name, request.Id), cancellationToken);
        if (isAuthorNameFound)
            return Result<bool>.Failure(new Error("Author Name Exists", Conflict, "Author Exists"));

        await _unitOfWork.Authors.Update(request, cancellationToken);
        return Result<bool>.Success(true);
    }
    private async Task<Author?> GetAuthorById(int id, CancellationToken? cancellationToken = default)
        => await _unitOfWork.Authors.GetBy(new GetAuthorById(id), cancellationToken);
}