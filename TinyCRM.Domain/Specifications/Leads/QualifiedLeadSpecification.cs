using System.Linq.Expressions;
using TinyCRM.Domain.Common.Enums;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Domain.Specifications.Base;

namespace TinyCRM.Domain.Specifications.Leads;

public class QualifiedLeadSpecification : Specification<Lead, int>, ISpecification<Lead, int>
{
    public override Expression<Func<Lead, bool>> ToExpression()
    {
        return x => x.Status == LeadStatus.Qualified;
    }
}