using System.Data;
namespace LibrarySystem.Application.Interfaces;

public interface ISqlDBTransaction
{
    Task InitilizeTransaction(IsolationLevel? isolationLevel = null);
    void CommitTransaction();
    void RollbackTransaction();
}
