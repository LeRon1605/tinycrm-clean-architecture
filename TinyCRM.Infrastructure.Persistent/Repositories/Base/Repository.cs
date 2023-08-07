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
        if (typeof(ISoftDeleteEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
        {
            ((ISoftDeleteEntity<TKey>)entity).IsDeleted = true;
            DbSet.Update(entity);
        }
        else
        {
            DbSet.Remove(entity);
        }
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        if (typeof(IAuditEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
        {
            ((IAuditEntity<TKey>)entity).CreatedDate = DateTime.UtcNow;
        }

        await DbSet.AddAsync(entity);
    }

    public virtual void Update(TEntity entity)
    {
        if (typeof(IAuditEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
        {
            ((IAuditEntity<TKey>)entity).UpdatedDate = DateTime.UtcNow;
        }

        DbSet.Update(entity);
    }

    public virtual void Insert(TEntity entity)
    {
        if (typeof(IAuditEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
        {
            ((IAuditEntity<TKey>)entity).CreatedDate = DateTime.UtcNow;
        }

        DbSet.Add(entity);
    }

    public virtual Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (typeof(IAuditEntity<TKey>).IsAssignableFrom(typeof(TEntity)))
            {
                ((IAuditEntity<TKey>)entity).CreatedDate = DateTime.UtcNow;
            }
        }

        return DbSet.AddRangeAsync(entities);
    }
}