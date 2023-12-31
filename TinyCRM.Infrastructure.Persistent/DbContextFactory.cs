﻿namespace TinyCRM.Infrastructure.Persistent;

public class DbContextFactory : IDisposable
{
    private bool _disposed;
    private Func<AppDbContext> _instanceFunc;
    private DbContext _dbContext;
    public DbContext DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());

    public DbContextFactory(Func<AppDbContext> dbContextFactory)
    {
        _instanceFunc = dbContextFactory;
    }

    public void Dispose()
    {
        if (!_disposed && _dbContext != null)
        {
            _disposed = true;
            _dbContext.Dispose();
        }
    }
}