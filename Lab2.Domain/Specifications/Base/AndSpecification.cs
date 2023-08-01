using Lab2.Domain.Base;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications;

public class AndSpecification<TEntity, TKey> : Specification<TEntity, TKey>, ISpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    private readonly Specification<TEntity, TKey> _left;

    private readonly Specification<TEntity, TKey> _right;

    public AndSpecification(Specification<TEntity, TKey> left, Specification<TEntity, TKey> right, bool isTracking = true) : base(isTracking)
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