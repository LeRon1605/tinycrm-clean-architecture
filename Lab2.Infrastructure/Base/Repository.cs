using Lab2.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Lab2.Infrastructure.Base;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
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

    public virtual Task<List<TEntity>> GetPagedListAsync(int skip, int take, Expression<Func<TEntity, bool>> expression, string? sorting = null, bool tracking = true, string? includeProps = null)
    {
        var queryable = tracking ? DbSet.AsQueryable() : DbSet.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeProps))
        {
            foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                queryable = queryable.Include(prop);
            }
        }

        if (!string.IsNullOrWhiteSpace(sorting))
        {
            queryable = queryable.OrderBy(sorting);
        }

        return queryable.Where(expression).Skip(skip).Take(take).ToListAsync();
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

    public virtual Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, string? includeProps = null)
    {
        var queryable = tracking ? DbSet.AsQueryable() : DbSet.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeProps))
        {
            foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                queryable = queryable.Include(prop);
            }
        }

        return queryable.FirstOrDefaultAsync(expression);
    }

    public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        if (expression != null)
        {
            return DbSet.AnyAsync(expression);
        }
        return DbSet.AnyAsync();
    }

    public virtual Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        if (expression != null)
        {
            return DbSet.CountAsync(expression);
        }
        return DbSet.CountAsync();
    }

    public virtual Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        return DbSet.AddRangeAsync(entities);
    }

    public async Task<TEntity?> FindByIdAsync(object id)
    {
        return await DbSet.FindAsync(id);
    }

    public Task<decimal> GetAverageAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>>? expression = null)
    {
        var queryable = DbSet.AsQueryable();
        if (expression != null)
        {
            queryable = queryable.Where(expression);
        }

        return queryable.AverageAsync(selector);
    }
}