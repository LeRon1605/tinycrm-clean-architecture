using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications.Deals;

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