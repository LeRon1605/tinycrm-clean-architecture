using Lab2.API.Middlewares;

namespace Lab2.API.Extensions;

public static class IServiceCollectionExtensions
{
    public static void InjectServices(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlingMiddleware>();
    }
}
