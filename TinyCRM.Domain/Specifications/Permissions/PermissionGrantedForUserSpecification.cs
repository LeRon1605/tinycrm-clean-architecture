using System.Linq.Expressions;
using TinyCRM.Domain.Common.Enums;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Specifications.Permissions;

public class PermissionGrantedForUserSpecification : Specification<PermissionGrant, int>, ISpecification<PermissionGrant, int>
{
    private readonly string _userId;
    private readonly int? _permissionId;

    public PermissionGrantedForUserSpecification(string userId, int? permissionId = null)
    {
        _userId = userId;
        _permissionId = permissionId;
    }

    public override Expression<Func<PermissionGrant, bool>> ToExpression()
    {
        return x => _userId == x.ProviderKey &&
                                 x.Provider == PermissionProvider.User &&
                                    (_permissionId == null || _permissionId == x.PermissionId);
    }
}