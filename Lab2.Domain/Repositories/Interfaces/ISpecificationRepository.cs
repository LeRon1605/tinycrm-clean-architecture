using Lab2.Domain.Base;
using Lab2.Domain.Specifications;

namespace Lab2.Domain.Repositories.Interfaces;

public interface ISpecificationRepository<TEntity, TKey> where TEntity : IEntity<TKey>
{
    Task<IList<TEntity>> GetPagedListAsync(int skip, int take, ISpecification<TEntity, TKey> specification, string? sorting = null);

    Task<IList<TEntity>> GetPagedListAsync(IPagingAndSortingSpecification<TEntity, TKey> specification);

    Task<TEntity?> FindAsync(ISpecification<TEntity, TKey> specification);

    Task<int> GetCountAsync(ISpecification<TEntity, TKey> specification);
}