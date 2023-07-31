using Lab2.Domain.Base;

namespace Lab2.Domain.Specifications;

public abstract class SortingSpecification<TEntity, TKey> : Specification<TEntity, TKey>, ISortingSpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    public string Sorting { get; }

    protected SortingSpecification(string sorting, bool isTracking = true) : base(isTracking)
    {
        Sorting = BuildSorting(sorting);
    }

    public virtual string BuildSorting(string sorting)
    {
        return sorting;
    }
}