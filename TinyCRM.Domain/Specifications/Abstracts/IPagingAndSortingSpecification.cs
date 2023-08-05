using TinyCRM.Domain.Entities.Base;

namespace TinyCRM.Domain.Specifications.Abstracts;

public interface IPagingAndSortingSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    int Take { get; set; }
    int Skip { get; set; }
    string Sorting { get; set; }

    string BuildSorting();

    new IPagingAndSortingSpecification<TEntity, TKey> And(ISpecification<TEntity, TKey> specification);
}