using System.Linq.Expressions;
using Lab2.Domain.Entities;

namespace Lab2.Domain.Specifications;

public class GetContactForAccountSpecification : Specification<Contact, int>, ISpecification<Contact, int>
{
    private readonly int _accountId;

    public GetContactForAccountSpecification(int accountId) : base(false)
    {
        _accountId = accountId;
    }

    public override Expression<Func<Contact, bool>> ToExpression()
    {
        return x => x.AccountId == _accountId;
    }
}