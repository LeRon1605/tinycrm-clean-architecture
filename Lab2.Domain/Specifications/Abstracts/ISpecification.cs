using Lab2.Domain.Base;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications;

public interface ISpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    bool IsTracking { get; set; }

    List<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; }

    List<string> IncludeStrings { get; set; }

    bool IsSatisfiedBy(TEntity entity);

    ISpecification<TEntity, TKey> And(ISpecification<TEntity, TKey> specification);

    Expression<Func<TEntity, bool>> ToExpression();
}