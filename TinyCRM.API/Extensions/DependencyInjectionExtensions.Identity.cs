using Microsoft.AspNetCore.Identity;
using TinyCRM.Application.Identity;
using TinyCRM.Application.Seeders.Interfaces;
using TinyCRM.Application.Services;
using TinyCRM.Infrastructure.Identity;
using TinyCRM.Infrastructure.Identity.Services;
using TinyCRM.Infrastructure.Persistent;

namespace TinyCRM.API.Extensions;

public static partial class DependencyInjectionExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddScoped<IDataSeeder, IdentityDataSeeder>();

        services.AddScoped<IApplicationUserManager, IdentityUserManager>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<ISignInManager, IdentitySignInManager>()
                .AddScoped<IRoleManager, IdentityRoleManager>();

        services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;

            // Signin settings.
            options.SignIn.RequireConfirmedEmail = false;
        });

        return services;
    }
}