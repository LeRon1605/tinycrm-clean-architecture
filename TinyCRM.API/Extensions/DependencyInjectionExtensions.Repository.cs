using TinyCRM.Application.Repositories;
using TinyCRM.Application.Repositories.Base;
using TinyCRM.Application.Seeders;
using TinyCRM.Application.Seeders.Interfaces;
using TinyCRM.Application.UnitOfWorks;
using TinyCRM.Infrastructure.Persistent.Repositories;
using TinyCRM.Infrastructure.Persistent.Repositories.Base;
using TinyCRM.Infrastructure.Persistent.UnitOfWorks;

namespace TinyCRM.API.Extensions;

public static partial class DependencyInjectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, DataSeeder>();
        services.AddScoped<IDataSeeder, PermissionDataSeeder>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>))
                .AddScoped(typeof(ISpecificationRepository<,>), typeof(SpecificationRepository<,>))
                .AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IAccountRepository, AccountRepository>()
                .AddScoped<IContactRepository, ContactRepository>()
                .AddScoped<ILeadRepository, LeadRepository>()
                .AddScoped<IDealRepository, DealRepository>()
                .AddScoped<IDealLineRepository, DealLineRepository>()
                .AddScoped<IPermissionRepository, PermissionRepository>()
                .AddScoped<IPermissionGrantRepository, PermissionGrantRepository>();

        return services;
    }
}