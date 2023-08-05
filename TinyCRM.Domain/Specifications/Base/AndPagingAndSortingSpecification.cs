using System.Linq.Expressions;
using TinyCRM.Domain.Entities.Base;
using TinyCRM.Domain.Specifications.Abstracts;

namespace TinyCRM.Domain.Specifications.Base;

public class AndPagingAndSortingSpecification<TEntity, TKey> : PagingAndSortingSpecification<TEntity, TKey>, IPagingAndSortingSpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    private readonly IPagingAndSortingSpecification<TEntity, TKey> _left;

    private readonly ISpecification<TEntity, TKey> _right;

    public AndPagingAndSortingSpecification(IPagingAndSortingSpecification<TEntity, TKey> left, ISpecification<TEntity, TKey> right) : base(left)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<TEntity, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();

        var parameter = Expression.Parameter(typeof(TEntity));
        var body = Expression.AndAlso(
            Expression.Invoke(leftExpression, parameter),
            Expression.Invoke(rightExpression, parameter)
        );

        return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
    }
}