using Lab2.API.Middlewares;
using Lab2.API.Services;
using Lab2.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Lab2.API.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlingMiddleware>();

        return services;
    }
}
