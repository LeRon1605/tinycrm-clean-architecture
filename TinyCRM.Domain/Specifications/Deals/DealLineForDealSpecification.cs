using System.Linq.Expressions;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Domain.Specifications.Base;

namespace TinyCRM.Domain.Specifications.Deals;

public class DealLineForDealSpecification : Specification<DealLine, int>, ISpecification<DealLine, int>
{
    private readonly int _dealId;

    public DealLineForDealSpecification(int dealId)
    {
        _dealId = dealId;
    }

    public override Expression<Func<DealLine, bool>> ToExpression()
    {
        return x => x.DealId == _dealId;
    }
}