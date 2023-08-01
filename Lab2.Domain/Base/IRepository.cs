using Lab2.Domain.Specifications;
using System.Linq.Expressions;

namespace Lab2.Domain.Base;

public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{
    Task<List<TEntity>> GetPagedListAsync(int skip, int take, Expression<Func<TEntity, bool>> expression, string? sorting = null, bool tracking = true, string? includeProps = null);

    Task<List<TEntity>> GetPagedListAsync(IPagingAndSortingSpecification<TEntity, TKey> specification);

    Task<TEntity?> FindByIdAsync(object id);

    Task<TEntity?> FindByIdAsync(object id, string includeProps, bool tracking = true);

    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, string? includeProps = null);

    Task InsertAsync(TEntity entity);

    Task InsertRangeAsync(IEnumerable<TEntity> entities);

    void Insert(TEntity entity);

    void Delete(TEntity entity);

    void Update(TEntity entity);

    Task<bool> IsExistingAsync(TKey id);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null);

    Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? expression = null);

    Task<int> GetCountAsync(ISpecification<TEntity, TKey> specification);

    Task<decimal> GetAverageAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>>? expression = null);
}

public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : IEntity<int>
{
}