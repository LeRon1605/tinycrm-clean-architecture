using TinyCRM.Application.Dtos.Permissions;

namespace TinyCRM.Application.Services.Interfaces;

public interface IPermissionService : IService<PermissionContent, int, PermissionDto>
{
    Task<IEnumerable<PermissionDto>> GetPermissionsForRoleAsync(string roleName);

    Task<IEnumerable<PermissionDto>> GetPermissionsForUserAsync(string userId);

    Task AddPermissionToRoleAsync(string roleName, AddPermissionDto addPermissionDto);

    Task AddPermissionToUserAsync(string userId, AddPermissionDto addPermissionDto);

    Task RemovePermissionFromRoleAsync(int id, string role);

    Task RemovePermissionFromUserAsync(int id, string userId);
}