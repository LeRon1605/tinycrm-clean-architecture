using TinyCRM.Application.Repositories.Base;
using TinyCRM.Domain.Entities.Base;

namespace TinyCRM.Infrastructure.Persistent.Repositories.Base;

public class Repository<TEntity, TKey> : ReadOnlyRepository<TEntity, TKey>, IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    public Repository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
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

    public virtual Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        return DbSet.AddRangeAsync(entities);
    }
}