using System.Linq.Expressions;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Domain.Specifications.Base;

namespace TinyCRM.Domain.Specifications.Leads;

public class LeadForAccountSpecification : Specification<Lead, int>, ISpecification<Lead, int>
{
    private readonly int _accountId;

    public LeadForAccountSpecification(int accountId)
    {
        _accountId = accountId;
    }

    public override Expression<Func<Lead, bool>> ToExpression()
    {
        return x => x.CustomerId == _accountId;
    }
}