using System.Linq.Expressions;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Specifications.Permissions;

public class PermissionFilterSpecification : PagingAndSortingSpecification<PermissionContent, int>, IPagingAndSortingSpecification<PermissionContent, int>
{
    private readonly string _name;

    public PermissionFilterSpecification(int page, int size, string sorting, string name) : base(page, size, sorting)
    {
        _name = name;
    }

    public override Expression<Func<PermissionContent, bool>> ToExpression()
    {
        return x => x.Name.Contains(_name);
    }
}