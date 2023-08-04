using Lab2.Domain.Base;

namespace Lab2.Domain.Specifications;

public interface IPagingAndSortingSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    int Take { get; set; }
    int Skip { get; set; }
    string Sorting { get; set; }

    string BuildSorting();

    new IPagingAndSortingSpecification<TEntity, TKey> And(ISpecification<TEntity, TKey> specification);
}