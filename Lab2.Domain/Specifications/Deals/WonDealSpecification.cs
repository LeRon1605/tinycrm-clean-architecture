using Lab2.Domain.Entities;
using Lab2.Domain.Enums;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications;

public class WonDealSpecification : Specification<Deal, int>, ISpecification<Deal, int>
{
    private readonly int? _dealId;
    public WonDealSpecification(int? dealId = null) : base(false)
    {
        _dealId = dealId;
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        return x => x.Status == DealStatus.Won && (_dealId == null || _dealId == x.Id);
    }
}