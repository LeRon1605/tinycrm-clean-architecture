using Lab2.Domain.Entities;

namespace Lab2.API.Services;

public interface IService<TEntity, TKey> where TEntity : IEntity<TKey>
{
}
