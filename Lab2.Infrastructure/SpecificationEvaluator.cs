using Lab2.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Lab2.Domain.Specifications;
using System.Linq.Dynamic.Core;

namespace Lab2.Infrastructure;

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