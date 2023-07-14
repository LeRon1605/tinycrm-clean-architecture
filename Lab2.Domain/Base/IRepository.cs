using System.Linq.Expressions;

namespace Lab2.Domain.Base;

public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression);
    Task InsertAsync(TEntity entity);
    Task InsertRangeAsync(IEnumerable<TEntity> entities);
    void Insert(TEntity entity);
    void Delete(TEntity entity);
    void Update(TEntity entity);
    Task<TEntity> FindByIdAsync(object id);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null);
    Task<List<TEntity>> GetListAsync(int skip, int take, Expression<Func<TEntity, bool>> expression);
}
