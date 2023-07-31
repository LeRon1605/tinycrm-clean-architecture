using System.Linq.Expressions;
using Lab2.Domain.Entities;
using Lab2.Domain.Enums;

namespace Lab2.Domain.Specifications;

public class WonDealSpecification : Specification<Deal, int>, ISpecification<Deal, int>
{
    public override Expression<Func<Deal, bool>> ToExpression()
    {
        return x => x.Status == DealStatus.Won;
    }
}