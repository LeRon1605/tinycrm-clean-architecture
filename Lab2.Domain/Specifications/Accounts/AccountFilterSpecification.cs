using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications;

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