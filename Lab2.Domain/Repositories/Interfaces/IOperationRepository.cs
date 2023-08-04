using Lab2.Domain.Base;

namespace Lab2.Domain.Repositories.Interfaces;

public interface IOperationRepository<in TEntity, TKey> where TEntity : IEntity<TKey>
{
    Task InsertAsync(TEntity entity);

    Task InsertRangeAsync(IEnumerable<TEntity> entities);

    void Insert(TEntity entity);

    void Delete(TEntity entity);

    void Update(TEntity entity);
}