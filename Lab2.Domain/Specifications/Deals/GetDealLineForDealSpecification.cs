using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications.Deals;

public class GetDealLineForDealSpecification : Specification<DealLine, int>, ISpecification<DealLine, int>
{
    private readonly int _dealId;

    public GetDealLineForDealSpecification(int dealId) : base(false)
    {
        _dealId = dealId;
    }

    public override Expression<Func<DealLine, bool>> ToExpression()
    {
        return x => x.DealId == _dealId;
    }
}