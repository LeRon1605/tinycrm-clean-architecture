using TinyCRM.Domain.Common.Enums;
using TinyCRM.Infrastructure.Persistent.Repositories.Base;

namespace TinyCRM.Infrastructure.Persistent.Repositories;

public class PermissionGrantRepository : Repository<PermissionGrant, int>, IPermissionGrantRepository
{
    public PermissionGrantRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public Task InsertForRoleAsync(string roleName, int permissionId)
    {
        return InsertAsync(new PermissionGrant()
        {
            PermissionId = permissionId,
            Provider = PermissionProvider.Role,
            ProviderKey = roleName,
        });
    }

    public Task InsertForUserAsync(string userId, int permissionId)
    {
        return InsertAsync(new PermissionGrant()
        {
            PermissionId = permissionId,
            Provider = PermissionProvider.User,
            ProviderKey = userId,
        });
    }

    public Task RemoveByUserAsync(string userId)
    {
        return DbSet.Where(x => x.Provider == PermissionProvider.User && x.ProviderKey == userId).ExecuteDeleteAsync();
    }

    public Task RemoveByRoleAsync(string roleName)
    {
        return DbSet.Where(x => x.Provider == PermissionProvider.Role && x.ProviderKey.ToLower() == roleName.ToLower()).ExecuteDeleteAsync();
    }
}