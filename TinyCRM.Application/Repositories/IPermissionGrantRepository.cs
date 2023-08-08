namespace TinyCRM.Application.Repositories;

public interface IPermissionGrantRepository : IRepository<PermissionGrant, int>
{
    Task InsertForRoleAsync(string roleName, int permissionId);

    Task InsertForUserAsync(string userId, int permissionId);

    Task RemoveByUserAsync(string userId);

    Task RemoveByRoleAsync(string roleName);
}