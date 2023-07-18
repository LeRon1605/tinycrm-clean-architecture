using Lab2.API.Middlewares;
using Lab2.Domain;

namespace Lab2.API.Extensions;

public static class IApplicationBuilderExtension
{
    public static void UseExceptionHandling(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static async Task SeedDataAsync(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var dataContributor = scope.ServiceProvider.GetRequiredService<DataContributor>();

        await dataContributor.SeedAsync();
    }
}