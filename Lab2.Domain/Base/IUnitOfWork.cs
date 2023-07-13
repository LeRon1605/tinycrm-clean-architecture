namespace Lab2.Domain.Base;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}
