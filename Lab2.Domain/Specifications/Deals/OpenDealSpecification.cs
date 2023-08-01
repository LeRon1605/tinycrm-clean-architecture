using Lab2.Domain.Entities;
using Lab2.Domain.Enums;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications;

public class OpenDealSpecification : Specification<Deal, int>, ISpecification<Deal, int>
{
    public override Expression<Func<Deal, bool>> ToExpression()
    {
        return x => x.Status == DealStatus.Open;
    }
}