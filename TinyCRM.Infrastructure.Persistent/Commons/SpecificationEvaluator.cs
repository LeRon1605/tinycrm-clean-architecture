using System.Linq.Dynamic.Core;
using TinyCRM.Domain.Entities.Base;
using TinyCRM.Domain.Specifications.Abstracts;

namespace TinyCRM.Infrastructure.Persistent.Commons;

public static class SpecificationEvaluator<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity, TKey> specification)
    {
        var queryable = specification.IsTracking ? inputQuery : inputQuery.AsNoTracking();

        if (specification.IncludeExpressions.Any())
        {
            queryable = specification.IncludeExpressions.Aggregate(queryable,
                                                            (current, include)
                                                                => current.Include(include));
        }

        if (specification.IncludeStrings.Any())
        {
            queryable = specification.IncludeStrings.Aggregate(queryable,
                (current, include)
                    => current.Include(include));
        }

        return queryable.Where(specification.ToExpression());
    }

    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, IPagingAndSortingSpecification<TEntity, TKey> specification)
    {
        var queryable = GetQuery(inputQuery, (ISpecification<TEntity, TKey>)specification);

        if (!string.IsNullOrWhiteSpace(specification.Sorting))
        {
            queryable = queryable.OrderBy(specification.BuildSorting());
        }

        return queryable.Skip(specification.Skip).Take(specification.Take);
    }
}