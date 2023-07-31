using System.Linq.Expressions;
using Lab2.Domain.Base;

namespace Lab2.Domain.Specifications;

public abstract class Specification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    public bool IsTracking { get; }
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    public List<string> IncludeStrings { get; }

    protected Specification(bool isTracking = true)
    {
        IsTracking = isTracking;
        IncludeExpressions = new List<Expression<Func<TEntity, object>>>();
        IncludeStrings = new List<string>();
    }

    public bool IsSatisfiedBy(TEntity entity)
    {
        return ToExpression().Compile().Invoke(entity);
    }

    public Specification<TEntity, TKey> And(Specification<TEntity, TKey> specification)
    {
        return new AndSpecification<TEntity, TKey>(this, specification);
    }

    public void AddInclude(Expression<Func<TEntity, object>> expression)
    {
        IncludeExpressions.Add(expression);
    }

    public void AddInclude(string prop)
    {
        IncludeStrings.Add(prop);
    }

    public abstract Expression<Func<TEntity, bool>> ToExpression();
}