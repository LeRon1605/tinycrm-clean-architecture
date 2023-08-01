using Lab2.Domain.Base;
using Lab2.Domain.Specifications.Base;

namespace Lab2.Domain.Specifications;

public abstract class PagingAndSortingSpecification<TEntity, TKey> : Specification<TEntity, TKey>, IPagingAndSortingSpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    public int Page { get; set; }
    public int Take { get; set; }
    public int Skip { get; set; }
    public string Sorting { get; set; }

    protected PagingAndSortingSpecification(int page, int size, string sorting, bool isTracking = true) : base(isTracking)
    {
        Page = page;
        Skip = (page - 1) * size;
        Take = size;
        Sorting = sorting;
    }

    protected PagingAndSortingSpecification(PagingAndSortingSpecification<TEntity, TKey> specification)
    {
        Page = specification.Page;
        Take = specification.Take;
        Skip = specification.Skip;
        Sorting = specification.Sorting;
        IncludeExpressions = specification.IncludeExpressions;
        IncludeStrings = specification.IncludeStrings;
        IsTracking = specification.IsTracking;
    }

    public new PagingAndSortingSpecification<TEntity, TKey> And(Specification<TEntity, TKey> specification)
    {
        return new AndPagingAndSortingSpecification<TEntity, TKey>(this, specification);
    }

    public virtual string BuildSorting()
    {
        return Sorting;
    }
}