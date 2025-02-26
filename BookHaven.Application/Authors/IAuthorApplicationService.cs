namespace BookHaven.Application.Authors;
public interface IAuthorApplicationService
{
    Task<Result<int>> Create(Author request, CancellationToken? cancellationToken = default);
    Task<Result<bool>> Update(Author request, CancellationToken? cancellationToken = default);
    Task<Result<bool>> Delete(int id, CancellationToken? cancellationToken = default);
    Task<Result<List<Author>>> GetAll(CancellationToken? cancellationToken = null);
    Task<Result<Author>> GetById(int id, CancellationToken? cancellationToken = null);
}