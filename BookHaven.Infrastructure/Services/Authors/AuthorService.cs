using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.Specification;
namespace BookHaven.Infrastructure.Services.Authors;
public class AuthorService(ISqlDataAccess _sqlDataAccess) : IAuthorService
{
    public async Task<List<T>> GetAll<T>(Specification<T> specification, CancellationToken? cancellationToken = default)
        => await _sqlDataAccess.LoadData(specification, cancellationToken);
}
