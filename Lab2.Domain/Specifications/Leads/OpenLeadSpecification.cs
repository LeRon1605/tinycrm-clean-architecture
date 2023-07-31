using System.Linq.Expressions;
using Lab2.Domain.Entities;
using Lab2.Domain.Enums;

namespace Lab2.Domain.Specifications.Leads;

public class OpenLeadSpecification : Specification<Lead, int>, ISpecification<Lead, int>
{
    public override Expression<Func<Lead, bool>> ToExpression()
    {
        return x => x.Status == LeadStatus.Open;
    }
}