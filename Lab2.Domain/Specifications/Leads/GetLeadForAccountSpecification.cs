using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications;

public class GetLeadForAccountSpecification : Specification<Lead, int>, ISpecification<Lead, int>
{
    private readonly int _accountId;

    public GetLeadForAccountSpecification(int accountId) : base(false)
    {
        _accountId = accountId;
    }

    public override Expression<Func<Lead, bool>> ToExpression()
    {
        return x => x.CustomerId == _accountId;
    }
}