using Microsoft.Extensions.Logging;
using TinyCRM.Application.Seeders.Interfaces;
using TinyCRM.Application.UnitOfWorks;
using TinyCRM.Domain.Common.Constants;
using TinyCRM.Domain.Specifications.Permissions;

namespace TinyCRM.Application.Seeders;

public class PermissionDataSeeder : IDataSeeder
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPermissionGrantRepository _permissionGrantRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PermissionDataSeeder> _logger;

    public PermissionDataSeeder(
        IPermissionGrantRepository permissionGrantRepository,
        IPermissionRepository permissionRepository,
        IUnitOfWork unitOfWork,
        ILogger<PermissionDataSeeder> logger)
    {
        _permissionGrantRepository = permissionGrantRepository;
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        await SyncPermissionAsync();
        await SeedPermissionForAdminRoleAsync();
        await SeedPermissionForUserRoleAsync();
    }

    private async Task SyncPermissionAsync()
    {
        _logger.LogInformation("Begin syncing permissions...");

        var permissions = await _permissionRepository.FindAllAsync();

        var deletePermissions = permissions.ExceptBy(Permissions.Provider.Select(x => x.Name), x => x.Name);
        var newPermissions = Permissions.Provider.ExceptBy(permissions.Select(x => x.Name), x => x.Name);
        foreach (var permission in deletePermissions)
        {
            _permissionRepository.Delete(permission);
        }

        foreach (var permission in newPermissions)
        {
            await _permissionRepository.InsertAsync(new PermissionContent(permission.Name, permission.Description));
        }

        await _unitOfWork.CommitAsync();
    }

    private async Task SeedPermissionForAdminRoleAsync()
    {
        _logger.LogInformation("Begin seeding not granted permissions for admin...");

        var notGrantedPermissions = await _permissionRepository.GetNotGrantedForAsync(new PermissionGrantedForRoleSpecification(AppRole.Admin));

        foreach (var permission in notGrantedPermissions)
        {
            await _permissionGrantRepository.InsertForRoleAsync(AppRole.Admin, permission.Id);
        }

        await _unitOfWork.CommitAsync();
    }

    private async Task SeedPermissionForUserRoleAsync()
    {
        _logger.LogInformation("Begin seeding not granted permissions for user...");

        var allowedModules = new[] { "Accounts", "Products", "Contacts", "Leads", "Deals" };
        var notGrantedPermissions = await _permissionRepository.GetNotGrantedForAsync(new PermissionGrantedForRoleSpecification(AppRole.User));
        foreach (var module in allowedModules)
        {
            var newPermissions = Permissions.GeneratePermissionsForModule(module)
                                                             .Where(x => x.Contains("View") && notGrantedPermissions.Any(permission => permission.Name == x));
            foreach (var permissionName in newPermissions)
            {
                await InsertPermissionToUserAsync(permissionName);
            }
        }

        if (notGrantedPermissions.All(x => x.Name != Permissions.Users.Edit))
        {
            await InsertPermissionToUserAsync(Permissions.Users.Edit);
        }

        await _unitOfWork.CommitAsync();
    }

    private async Task InsertPermissionToUserAsync(string permissionName)
    {
        var permission = await _permissionRepository.FindByNameAsync(permissionName);
        await _permissionGrantRepository.InsertForRoleAsync(AppRole.User, permission.Id);
    }
}