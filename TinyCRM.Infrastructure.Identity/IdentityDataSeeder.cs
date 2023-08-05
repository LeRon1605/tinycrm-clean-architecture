using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TinyCRM.Application.Common.Seeders;
using TinyCRM.Domain.Common.Constants;

namespace TinyCRM.Infrastructure.Identity;

public class IdentityDataSeeder : IDataSeeder
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger _logger;

    public IdentityDataSeeder(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
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

                var userRole = new IdentityRole(AppRole.User);
                var adminRole = new IdentityRole(AppRole.Admin);
                var user = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    FullName = "admin"
                };

                await _roleManager.CreateAsync(userRole);
                await _roleManager.CreateAsync(adminRole);

                await _userManager.CreateAsync(user, "admin123");
                await _userManager.AddToRoleAsync(user, AppRole.Admin);

                _logger.LogInformation("Seed identity data successfully!");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Seeding identity data failed!", ex);
        }
    }
}