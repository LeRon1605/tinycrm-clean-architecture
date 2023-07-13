using Lab2.Domain.Base;

namespace Lab2.Infrastructure.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContextFactory _dbContextFactory;
    public UnitOfWork(DbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public Task<int> CommitAsync()
    {
        return _dbContextFactory.DbContext.SaveChangesAsync();
    }
}
