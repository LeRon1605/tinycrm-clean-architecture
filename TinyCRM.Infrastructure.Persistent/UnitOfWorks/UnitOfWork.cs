using Microsoft.EntityFrameworkCore.Storage;
using TinyCRM.Application.Common.UnitOfWorks;

namespace TinyCRM.Infrastructure.Persistent.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContextFactory _dbContextFactory;
    private IDbContextTransaction _transaction;

    public UnitOfWork(DbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public Task<int> CommitAsync()
    {
        return _dbContextFactory.DbContext.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _dbContextFactory.DbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync(bool autoRollbackOnFail)
    {
        if (_transaction != null)
        {
            try
            {
                await CommitAsync();
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
            }
            catch
            {
                if (autoRollbackOnFail)
                {
                    await RollbackTransactionAsync();
                }
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }
}