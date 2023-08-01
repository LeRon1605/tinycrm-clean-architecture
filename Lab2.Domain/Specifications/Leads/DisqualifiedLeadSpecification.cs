using Lab2.Domain.Entities;
using Lab2.Domain.Enums;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications;

public class DisqualifiedLeadSpecification : Specification<Lead, int>, ISpecification<Lead, int>
{
    public override Expression<Func<Lead, bool>> ToExpression()
    {
        return x => x.Status == LeadStatus.Disqualified;
    }
}