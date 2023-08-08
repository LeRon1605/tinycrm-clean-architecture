using System.Linq.Expressions;

namespace TinyCRM.Domain.Specifications.Base;

public class AndSpecification<TEntity, TKey> : Specification<TEntity, TKey>, ISpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    private readonly ISpecification<TEntity, TKey> _left;

    private readonly ISpecification<TEntity, TKey> _right;

    public AndSpecification(ISpecification<TEntity, TKey> left, ISpecification<TEntity, TKey> right) : base(left)
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