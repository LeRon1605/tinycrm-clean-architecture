using System.Linq.Expressions;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Specifications.Contacts;

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