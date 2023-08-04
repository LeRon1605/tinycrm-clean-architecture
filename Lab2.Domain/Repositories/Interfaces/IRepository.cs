using Lab2.Domain.Base;

namespace Lab2.Domain.Repositories.Interfaces;

public interface IRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>, IOperationRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{
}