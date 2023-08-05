using System.Linq.Expressions;
using TinyCRM.Domain.Entities.Base;

namespace TinyCRM.Domain.Specifications.Abstracts;

public interface ISpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    bool IsTracking { get; set; }

    List<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; }

    List<string> IncludeStrings { get; set; }

    bool IsSatisfiedBy(TEntity entity);

    ISpecification<TEntity, TKey> And(ISpecification<TEntity, TKey> specification);

    Expression<Func<TEntity, bool>> ToExpression();
}