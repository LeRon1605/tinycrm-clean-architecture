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

    public virtual Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.Where(expression).ToListAsync();
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public virtual void Insert(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public virtual Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.FirstOrDefaultAsync(expression);
    }

    public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression != null)
        {
            return DbSet.AnyAsync(expression);
        }
        return DbSet.AnyAsync();
    }

    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression != null)
        {
            return DbSet.CountAsync(expression);
        }
        return DbSet.CountAsync();
    }

    public virtual Task<List<TEntity>> GetListAsync(int skip, int take, Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.Skip(skip).Take(take).Where(expression).ToListAsync();
    }

    public virtual Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        return DbSet.AddRangeAsync(entities);
    }

    public async Task<TEntity> FindByIdAsync(object id)
    {
        return await DbSet.FindAsync(id);
    }
}
