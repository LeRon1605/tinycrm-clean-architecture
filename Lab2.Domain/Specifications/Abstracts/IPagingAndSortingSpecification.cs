using Lab2.Domain.Base;

namespace Lab2.Domain.Specifications;

public interface IPagingAndSortingSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    int Take { get; }
    int Skip { get; }
    string Sorting { get; set; }
    string BuildSorting();
    PagingAndSortingSpecification<TEntity, TKey> And(Specification<TEntity, TKey> specification);
}