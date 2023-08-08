using System.Linq.Expressions;
using TinyCRM.Domain.Common.Enums;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Specifications.Permissions;

public class PermissionGrantedForRoleSpecification : Specification<PermissionGrant, int>, ISpecification<PermissionGrant, int>
{
    private readonly string _roleName;
    private readonly int? _permissionId;

    public PermissionGrantedForRoleSpecification(string roleName, int? permissionId = null)
    {
        _roleName = roleName;
        _permissionId = permissionId;
    }

    public override Expression<Func<PermissionGrant, bool>> ToExpression()
    {
        return x => x.ProviderKey.ToLower() == _roleName.ToLower() &&
                                 x.Provider == PermissionProvider.Role &&
                                 (_permissionId == null || _permissionId == x.PermissionId);
    }
}