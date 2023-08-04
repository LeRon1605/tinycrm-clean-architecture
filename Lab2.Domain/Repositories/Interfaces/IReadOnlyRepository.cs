using System.Linq.Expressions;
using Lab2.Domain.Base;

namespace Lab2.Domain.Repositories.Interfaces;

public interface IReadOnlyRepository<TEntity, TKey> : ISpecificationRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{
    Task<IList<TEntity>> GetPagedListAsync(int skip, int take, Expression<Func<TEntity, bool>> expression, string? sorting = null, bool tracking = true, string? includeProps = null);

    Task<TEntity?> FindByIdAsync(object id, string? includeProps = null, bool tracking = true);

    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, string? includeProps = null);

    Task<bool> IsExistingAsync(TKey id);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null);

    Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? expression = null);

    Task<decimal> GetAverageAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>>? expression = null);
}