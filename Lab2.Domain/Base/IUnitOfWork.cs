namespace Lab2.Domain.Base;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync(bool autoRollbackOnFail = true);
    Task RollbackTransactionAsync();
}