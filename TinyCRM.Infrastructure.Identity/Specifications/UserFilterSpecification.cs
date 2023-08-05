using System.Linq.Expressions;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Domain.Specifications.Base;

namespace TinyCRM.Infrastructure.Identity.Specifications;

public class UserFilterSpecification : PagingAndSortingSpecification<ApplicationUser, string>, IPagingAndSortingSpecification<ApplicationUser, string>
{
    private readonly string _name;

    public UserFilterSpecification(int page, int size, string name, string sorting) : base(page, size, sorting)
    {
        _name = name;
    }

    public override Expression<Func<ApplicationUser, bool>> ToExpression()
    {
        return x => x.FullName.Contains(_name);
    }
}