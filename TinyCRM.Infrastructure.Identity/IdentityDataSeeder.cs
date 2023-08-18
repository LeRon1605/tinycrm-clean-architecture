using Microsoft.Extensions.Logging;
using TinyCRM.Application.Seeders.Interfaces;
using TinyCRM.Domain.Common.Constants;

namespace TinyCRM.Infrastructure.Identity;

public class IdentityDataSeeder : IDataSeeder
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<IdentityDataSeeder> _logger;

    public IdentityDataSeeder(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ILogger<IdentityDataSeeder> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            if (
                !_roleManager.Roles.Any() &&
                !_userManager.Users.Any()
            )
            {
                _logger.LogInformation("Begin seeding identity data...");

                await SeedAdminRoleAsync();
                await SeedUserRoleAsync();

                await SeedDefaultAdminAccountAsync();

                _logger.LogInformation("Seed identity data successfully!");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Seeding identity data failed!", ex);
        }
    }

    private async Task SeedAdminRoleAsync()
    {
        var adminRole = new ApplicationRole(AppRole.Admin);
        var result = await _roleManager.CreateAsync(adminRole);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Seeding admin role failed!");
        }
    }

    private async Task SeedUserRoleAsync()
    {
        var userRole = new ApplicationRole(AppRole.User);
        var result = await _roleManager.CreateAsync(userRole);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Seeding user role failed!");
        }
    }

    private async Task SeedDefaultAdminAccountAsync()
    {
        var user = new ApplicationUser()
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            FullName = "admin"
        };

        await _userManager.CreateAsync(user, "admin123");
        await _userManager.AddToRoleAsync(user, AppRole.Admin);
    }
}