﻿using Lab2.Domain.Base;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications;

public interface ISpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    bool IsTracking { get; }

    List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

    List<string> IncludeStrings { get; }

    bool IsSatisfiedBy(TEntity entity);

    Specification<TEntity, TKey> And(Specification<TEntity, TKey> specification);

    Expression<Func<TEntity, bool>> ToExpression();
}