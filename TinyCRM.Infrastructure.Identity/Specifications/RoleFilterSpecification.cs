using System.Linq.Expressions;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Domain.Specifications.Base;

namespace TinyCRM.Infrastructure.Identity.Specifications;

public class RoleFilterSpecification : PagingAndSortingSpecification<ApplicationRole, string>, IPagingAndSortingSpecification<ApplicationRole, string>
{
    private readonly string _name;

    public RoleFilterSpecification(int page, int size, string name, string sorting) : base(page, size, sorting)
    {
        _name = name;
    }

    public override Expression<Func<ApplicationRole, bool>> ToExpression()
    {
        return x => x.Name.Contains(_name);
    }
}