using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Repositories;
using BookHaven.Domain.Specification;

namespace BookHaven.Infrastructure.Repositories;
public class GenericSpecificationReadRepository(ISqlDataAccess sqlDataAccess) : IGenericSpecificationReadRepository
{
    private readonly ISqlDataAccess _sqlDataAccess = sqlDataAccess;
    public async Task<List<TResult>> GetAll<TResult>(Specification<TResult> param, CancellationToken? cancellationToken)
        => await _sqlDataAccess.LoadData(param, cancellationToken);
    public async Task<TResult?> GetBy<TResult>(Specification<TResult> param, CancellationToken? cancellationToken)
        => await _sqlDataAccess.LoadFirstOrDefault(param, cancellationToken);
}