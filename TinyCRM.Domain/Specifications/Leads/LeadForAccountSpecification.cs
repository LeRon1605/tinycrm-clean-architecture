using System.Linq.Expressions;
using TinyCRM.Domain.Entities;

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