using System.Linq.Expressions;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Specifications.Permissions;

public class PermissionGrantedExceptSpecification : Specification<PermissionGrant, int>, ISpecification<PermissionGrant, int>
{
    private readonly IEnumerable<int> _exceptPermissionIds;

    public PermissionGrantedExceptSpecification(IEnumerable<int> exceptPermissionIds)
    {
        _exceptPermissionIds = exceptPermissionIds;
    }

    public PermissionGrantedExceptSpecification(IEnumerable<PermissionContent> exceptPermissions)
    {
        _exceptPermissionIds = exceptPermissions.Select(x => x.Id);
    }

    public override Expression<Func<PermissionGrant, bool>> ToExpression()
    {
        return x => _exceptPermissionIds.All(permission => permission != x.PermissionId);
    }
}