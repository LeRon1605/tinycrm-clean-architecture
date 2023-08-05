using System.Linq.Expressions;
using TinyCRM.Domain.Common.Enums;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Domain.Specifications.Base;

namespace TinyCRM.Domain.Specifications.Deals;

public class WonDealSpecification : Specification<Deal, int>, ISpecification<Deal, int>
{
    public WonDealSpecification()
    {
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        return x => x.Status == DealStatus.Won;
    }
}