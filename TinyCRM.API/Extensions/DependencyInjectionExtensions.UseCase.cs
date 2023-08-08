using TinyCRM.API.Common.ExceptionHandlers;
using TinyCRM.Application.Services;

namespace TinyCRM.API.Extensions;

public static partial class DependencyInjectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IExceptionHandler, ExceptionHandler>();

        services.AddScoped<IAccountService, AccountService>()
                .AddScoped<IContactService, ContactService>()
                .AddScoped<ILeadService, LeadService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IDealService, DealService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<ITokenProvider, JwtTokenProvider>()
                .AddScoped<IPermissionService, PermissionService>()
                .AddScoped<IRoleService, RoleService>();

        return services;
    }
}