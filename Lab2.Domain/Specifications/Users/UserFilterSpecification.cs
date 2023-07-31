using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications.Users;

public class UserFilterSpecification : PagingAndSortingSpecification<User, string>, IPagingAndSortingSpecification<User, string>
{
    private readonly string _name;

    public UserFilterSpecification(int page, int size, string name, string sorting) : base(page, size, sorting, false)
    {
        _name = name;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return x => x.FullName.Contains(_name);
    }
}