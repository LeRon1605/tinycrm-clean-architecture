using EntityFramework.Exceptions.SqlServer;
using Lab2.API.Middlewares;
using Lab2.API.Services;
using Lab2.Domain;
using Lab2.Domain.Base;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure;
using Lab2.Infrastructure.Base;
using Lab2.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab2.API.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseExceptionProcessor();
            options.EnableSensitiveDataLogging(true);
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<DbContextFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ExceptionHandlingMiddleware>();

        services.AddScoped<IAccountService, AccountService>()
                .AddScoped<IContactService, ContactService>()
                .AddScoped<ILeadService, LeadService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IDealService, DealService>()
                .AddScoped<ILineService, LineService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<DataContributor>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IAccountRepository, AccountRepository>()
                .AddScoped<IContactRepository, ContactRepository>()
                .AddScoped<ILeadRepository, LeadRepository>()
                .AddScoped<IDealRepository, DealRepository>()
                .AddScoped<IDealLineRepository, DealLineRepository>();

        return services;
    }
}