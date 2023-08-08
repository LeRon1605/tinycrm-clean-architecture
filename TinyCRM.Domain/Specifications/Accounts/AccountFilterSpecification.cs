using System.Linq.Expressions;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Specifications.Accounts;

public class AccountFilterSpecification : PagingAndSortingSpecification<Account, int>, IPagingAndSortingSpecification<Account, int>
{
    private readonly string _name;

    public AccountFilterSpecification(int page, int size, string name, string sorting) : base(page, size, sorting, false)
    {
        _name = name;
    }

    public override Expression<Func<Account, bool>> ToExpression()
    {
        return x => x.Name.Contains(_name);
    }
}