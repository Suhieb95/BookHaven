using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Specification;
namespace LibrarySystem.Infrastructure.Services.Authors;
public class AuthorService(ISqlDataAccess _sqlDataAccess) : IAuthorService
{
    public async Task<List<T>> GetAll<T>(Specification<T> specification, CancellationToken? cancellationToken = default)
        => await _sqlDataAccess.LoadData(specification, cancellationToken);
}
