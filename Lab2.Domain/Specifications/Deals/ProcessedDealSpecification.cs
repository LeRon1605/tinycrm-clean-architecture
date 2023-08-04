using Lab2.Domain.Entities;
using System.Linq.Expressions;
using Lab2.Domain.Enums;

namespace Lab2.Domain.Specifications.Deals;

public class ProcessedDealSpecification : Specification<Deal, int>, ISpecification<Deal, int>
{
    private readonly int? _dealId;
    public ProcessedDealSpecification(int? dealId = null)
    {
        _dealId = dealId;
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        return x => x.Status != DealStatus.Open && (_dealId == null || _dealId == x.Id);
    }
}