using System.Linq.Expressions;
namespace LibrarySystem.Application.Interfaces.Database;
public interface INoSqlTransaction
{
    Task<TDocument> SaveInTransaction<TDocument>(TDocument parameters, CancellationToken? cancellationToken = null) where TDocument : class;
    Task<List<TDocument>> SaveManyInTransaction<TDocument>(List<TDocument> parameters, CancellationToken? cancellationToken = null) where TDocument : class;
    Task<bool> UpdateInTransaction<TDocument>(Expression<Func<TDocument, bool>> parameters, TDocument document, CancellationToken? cancellationToken = null) where TDocument : class;
    Task<bool> DeleteInTransaction<TDocument>(Expression<Func<TDocument, bool>> parameters, CancellationToken? cancellationToken = null) where TDocument : class;
    Task InitilizeTransaction();
    Task CommitTransaction(CancellationToken? cancellationToken = null);
    Task RollbackTransaction(CancellationToken? cancellationToken = null);
}
