using System.Data;
namespace BookHaven.Application.Interfaces.Database;
public interface ISqlDBTransaction
{
    Task InitilizeTransaction(IsolationLevel? isolationLevel = null);
    void CommitTransaction();
    void RollbackTransaction();
}