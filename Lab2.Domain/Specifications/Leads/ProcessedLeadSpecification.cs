using Lab2.Domain.Entities;
using System.Linq.Expressions;
using Lab2.Domain.Enums;

namespace Lab2.Domain.Specifications;

public class ProcessedLeadSpecification : Specification<Lead, int>, ISpecification<Lead, int>
{
    public override Expression<Func<Lead, bool>> ToExpression()
    {
        return x => x.Status == LeadStatus.Qualified || x.Status == LeadStatus.Disqualified;
    }
}