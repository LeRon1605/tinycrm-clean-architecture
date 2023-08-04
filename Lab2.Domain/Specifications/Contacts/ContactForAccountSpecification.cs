using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications;

public class ContactForAccountSpecification : Specification<Contact, int>, ISpecification<Contact, int>
{
    private readonly int _accountId;

    public ContactForAccountSpecification(int accountId)
    {
        _accountId = accountId;
    }

    public override Expression<Func<Contact, bool>> ToExpression()
    {
        return x => x.AccountId == _accountId;
    }
}