using Lab2.Domain.Entities;
using Lab2.Domain.Enums;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications;

public class LostDealSpecification : Specification<Deal, int>, ISpecification<Deal, int>
{
    private readonly int? _dealId;
    public LostDealSpecification(int? dealId = null) : base(false)
    {
        _dealId = dealId;
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        return x => x.Status == DealStatus.Lost && (_dealId == null || _dealId == x.Id);
    }
}