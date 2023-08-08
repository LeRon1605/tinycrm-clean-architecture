namespace TinyCRM.Domain.Specifications.Base;

public abstract class PagingAndSortingSpecification<TEntity, TKey> : Specification<TEntity, TKey>, IPagingAndSortingSpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    public int Take { get; set; }
    public int Skip { get; set; }
    public string Sorting { get; set; }

    protected PagingAndSortingSpecification(int page, int size, string sorting, bool isTracking = false) : base(isTracking)
    {
        Skip = (page - 1) * size;
        Take = size;
        Sorting = sorting;
    }

    protected PagingAndSortingSpecification(IPagingAndSortingSpecification<TEntity, TKey> specification)
    {
        Take = specification.Take;
        Skip = specification.Skip;
        Sorting = specification.Sorting;
        IncludeExpressions = specification.IncludeExpressions;
        IncludeStrings = specification.IncludeStrings;
        IsTracking = specification.IsTracking;
    }

    public new IPagingAndSortingSpecification<TEntity, TKey> And(ISpecification<TEntity, TKey> specification)
    {
        return new AndPagingAndSortingSpecification<TEntity, TKey>(this, specification);
    }

    public virtual string BuildSorting()
    {
        return Sorting;
    }
}