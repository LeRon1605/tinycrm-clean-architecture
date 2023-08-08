﻿using System.Linq.Expressions;
using TinyCRM.Domain.Common.Enums;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Specifications.Deals;

public class ProcessedDealSpecification : Specification<Deal, int>, ISpecification<Deal, int>
{
    public ProcessedDealSpecification()
    {
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        return x => x.Status != DealStatus.Open;
    }
}