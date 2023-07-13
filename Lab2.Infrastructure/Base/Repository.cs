using Lab2.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lab2.Infrastructure.Base;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
{
    private readonly DbContextFactory _dbContextFactory;
    private DbSet<TEntity> _dbSet;
    protected DbSet<TEntity> DbSet
    {
        get
        {
            return _dbSet ?? (_dbSet = _dbContextFactory.DbContext.Set<TEntity>());
        }
    }

    public Repository(DbContextFactory dbContextFactory) 
    {
        _dbContextFactory = dbContextFactory;
    } 

    public void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.Where(expression).ToListAsync();
    }

    public async Task InsertAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public void Insert(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.FirstOrDefaultAsync(expression);
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.AnyAsync(expression);
    }

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression != null)
        {
            return DbSet.CountAsync(expression);
        }
        return DbSet.CountAsync();
    }

    public Task<List<TEntity>> GetListAsync(int skip, int take, Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.Skip(skip).Take(take).ToListAsync();
    }
}
