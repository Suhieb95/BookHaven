using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Books;
using BookHaven.Domain.Entities;
using BookHaven.Domain.Specification;
namespace BookHaven.Infrastructure.Services.Authors;
public class AuthorService(ISqlDataAccess _sqlDataAccess, IMssqlDbTransaction _mssqlDbTransaction) : IAuthorService
{
    private readonly string _upsertAuthor = "UpsertAuthor";
    public async Task<int> Add(Author entity, CancellationToken? cancellationToken = null)
        => await _sqlDataAccess.SaveData<int>(_upsertAuthor, entity, StoredProcedure, cancellationToken);
    public async Task Delete(int id, CancellationToken? cancellationToken = null)
    {
        const string Sql = "SPDeleteAuthor";
        await _sqlDataAccess.SaveData(Sql, new { id }, StoredProcedure, cancellationToken);
    }
    public async Task Update(Author entity, CancellationToken? cancellationToken = null)
        => await _sqlDataAccess.SaveData(_upsertAuthor, entity, StoredProcedure, cancellationToken);
    public async Task<List<T>> GetAll<T>(Specification<T> specification, CancellationToken? cancellationToken = default)
        => await _sqlDataAccess.LoadData(specification, cancellationToken);
    public async Task UpdateBookAuthors(UpdateBookAuthorsRequest request, CancellationToken? cancellationToken = null)
    {
        const string DeleteOldSql = "DELETE FROM BookAuthors WHERE BookId = @BookId AND AuthorId NOT IN (@AuthorIds)";
        await _mssqlDbTransaction.SaveDataInTransaction(DeleteOldSql, new { request.BookId, AuthorIds = request.Authors }, cancellationToken: cancellationToken);

        const string Sql = "SPUpdateBookAuthor";
        var tasks = request.Authors
                                            .Select(x => _mssqlDbTransaction.SaveDataInTransaction(Sql, new { BookId = request.BookId, AuthorId = x }, StoredProcedure, cancellationToken));
        await Task.WhenAll(tasks);
    }
}