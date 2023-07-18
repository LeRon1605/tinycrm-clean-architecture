using System.Linq.Expressions;

namespace Lab2.API.Extensions;

public static class ExpressionExtensions
{
    public static Expression<Func<TEntity, bool>> JoinWith<TEntity>(this Expression<Func<TEntity, bool>> first, Expression<Func<TEntity, bool>> second)
    {
        var parameter = Expression.Parameter(typeof(TEntity));

        var body = Expression.AndAlso(
            Expression.Invoke(first, parameter),
            Expression.Invoke(second, parameter)
        );

        return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
    }
}
