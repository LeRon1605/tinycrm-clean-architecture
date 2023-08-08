using System.Reflection;
using TinyCRM.Application.Mapper;
using TinyCRM.Infrastructure.Identity.Mapper;

namespace TinyCRM.API.Extensions;

public static partial class DependencyInjectionExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(TinyCrmMapper)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(IdentityMapper)));

        return services;
    }
}