using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Books;
using BookHaven.Domain.Entities;
namespace BookHaven.Infrastructure.Services.Authors;
public class AuthorService(ISqlDataAccess sqlDataAccess, IMssqlDbTransaction mssqlDbTransaction) : GenericSpecificationReadRepository(sqlDataAccess), IAuthorService
{
    private readonly ISqlDataAccess _sqlDataAccess = sqlDataAccess;
    private readonly IMssqlDbTransaction _mssqlDbTransaction = mssqlDbTransaction;
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
    public async Task UpdateBookAuthors(UpdateBookAuthorsRequest request, CancellationToken? cancellationToken = null)
    {
        const string DeleteOldSql = "DELETE FROM BookAuthors WHERE BookId = @BookId AND AuthorId NOT IN @AuthorIds";
        await _mssqlDbTransaction.SaveDataInTransaction(DeleteOldSql, new { request.BookId, AuthorIds = request.Authors }, cancellationToken: cancellationToken);
        const string Sql = "SPUpdateBookAuthors";
        var tasks = request.Authors
                                            .Select(authorId => _mssqlDbTransaction.SaveDataInTransaction(Sql, new { BookId = request.BookId, AuthorId = authorId }, StoredProcedure, cancellationToken));
        await Task.WhenAll(tasks);
    }
}