using System.Linq.Expressions;

namespace Lab2.Domain.Base;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<List<TEntity>> GetPagedListAsync(int skip, int take, Expression<Func<TEntity, bool>> expression, string? sorting = null, bool tracking = true, string? includeProps = null);

    Task<TEntity?> FindByIdAsync(object id);

    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, string? includeProps = null);

    Task InsertAsync(TEntity entity);

    Task InsertRangeAsync(IEnumerable<TEntity> entities);

    void Insert(TEntity entity);

    void Delete(TEntity entity);

    void Update(TEntity entity);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null);

    Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? expression = null);

    Task<decimal> GetAverageAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>>? expression = null);
}