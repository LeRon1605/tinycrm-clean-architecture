using System.Linq.Expressions;
using Lab2.Domain.Base;
using Lab2.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Infrastructure.Repositories;

public class ReadOnlyRepository<TEntity, TKey> : SpecificationRepository<TEntity, TKey>, IReadOnlyRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    public ReadOnlyRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public async Task<IList<TEntity>> GetPagedListAsync(int skip, int take, Expression<Func<TEntity, bool>> expression, string? sorting = null, bool tracking = true, string? includeProps = null)
    {
        var queryable = new AppQueryableBuilder<TEntity, TKey>(DbSet, tracking)
                                    .IncludeProp(includeProps)
                                    .ApplyFilter(expression)
                                    .ApplySorting(sorting)
                                    .Build();

        return await queryable.Skip(skip).Take(take).ToListAsync();
    }

    public Task<TEntity?> FindByIdAsync(object id, string? includeProps = null, bool tracking = true)
    {
        var queryable = new AppQueryableBuilder<TEntity, TKey>(DbSet, tracking)
                                    .IncludeProp(includeProps)
                                    .Build();

        return queryable.FirstOrDefaultAsync(x => id.Equals(x.Id));
    }

    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true,
        string? includeProps = null)
    {
        var queryable = new AppQueryableBuilder<TEntity, TKey>(DbSet, tracking)
                                    .IncludeProp(includeProps)
                                    .Build();

        return queryable.FirstOrDefaultAsync(expression);
    }

    public Task<bool> IsExistingAsync(TKey id)
    {
        ArgumentNullException.ThrowIfNull(id);

        return DbSet.AnyAsync(x => id.Equals(x.Id));
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        return expression != null ? DbSet.AnyAsync(expression) : DbSet.AnyAsync();
    }

    public Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        return expression != null ? DbSet.CountAsync(expression) : DbSet.CountAsync();
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