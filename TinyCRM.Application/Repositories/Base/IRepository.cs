using TinyCRM.Domain.Entities.Base;

namespace TinyCRM.Application.Repositories.Base;

public interface IRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>, IOperationRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{
}