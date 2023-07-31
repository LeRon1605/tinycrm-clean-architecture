using Lab2.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Lab2.Domain.Specifications;

namespace Lab2.Infrastructure.Base;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
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
        var queryable = new AppQueryableBuilder<TEntity, TKey>(DbSet, tracking)
                                        .IncludeProp(includeProps)
                                        .ApplyFilter(expression)
                                        .ApplySorting(sorting)
                                        .Build(); 

        return queryable.Skip(skip).Take(take).ToListAsync();
    }

    public Task<List<TEntity>> GetPagedListAsync(IPagingAndSortingSpecification<TEntity, TKey> specification)
    {
        return SpecificationEvaluator<TEntity, TKey>.GetQuery(DbSet.AsQueryable(), specification).ToListAsync();
    }

    public Task<List<TEntity>> GetPagedListAsync(int skip, int take, ISpecification<TEntity, TKey> specification, string? sorting = null, bool tracking = true,
        string? includeProps = null)
    {
        return GetPagedListAsync(skip, take, specification.ToExpression(), sorting, tracking, includeProps);
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public Task<bool> IsExistingAsync(TKey id)
    {
        return DbSet.AnyAsync(x => x.Id.Equals(id));
    }

    public virtual void Insert(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public Task<TEntity?> FindByIdAsync(object id, string includeProps, bool tracking)
    {
        var queryable = new AppQueryableBuilder<TEntity, TKey>(DbSet, tracking)
                                        .IncludeProp(includeProps)
                                        .Build();

        return queryable.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public virtual Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, string? includeProps = null)
    {
        var queryable = new AppQueryableBuilder<TEntity, TKey>(DbSet, tracking)
                                        .IncludeProp(includeProps)
                                        .Build();

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

    public Task<int> GetCountAsync(ISpecification<TEntity, TKey> specification)
    {
        return GetCountAsync(specification.ToExpression());
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

public class Repository<TEntity> : Repository<TEntity, int>, IRepository<TEntity> where TEntity : class, IEntity<int>
{
    public Repository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}