using Lab2.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Lab2.Infrastructure;

public class AppQueryableBuilder<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    private IQueryable<TEntity> _queryable;

    public AppQueryableBuilder(DbSet<TEntity> dbSet, bool tracking)
    {
        _queryable = tracking ? dbSet.AsQueryable() : dbSet.AsNoTracking();
    }

    public AppQueryableBuilder<TEntity, TKey> ApplyFilter(Expression<Func<TEntity, bool>> expression)
    {
        _queryable = _queryable.Where(expression);
        return this;
    }

    public AppQueryableBuilder<TEntity, TKey> ApplySorting(string? sorting)
    {
        if (!string.IsNullOrWhiteSpace(sorting))
        {
            _queryable = _queryable.OrderBy(sorting);
        }
        return this;
    }

    public AppQueryableBuilder<TEntity, TKey> IncludeProp(Expression<Func<TEntity, object>> includeProps)
    {
        _queryable = _queryable.Include(includeProps);
        return this;
    }

    public AppQueryableBuilder<TEntity, TKey> IncludeProp(string? includeProps)
    {
        if (!string.IsNullOrWhiteSpace(includeProps))
        {
            foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                _queryable = _queryable.Include(prop);
            }
        }

        return this;
    }

    public IQueryable<TEntity> Build()
    {
        return _queryable;
    }
}