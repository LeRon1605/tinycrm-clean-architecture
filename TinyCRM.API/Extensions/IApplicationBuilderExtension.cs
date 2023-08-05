using Microsoft.AspNetCore.Diagnostics;
using TinyCRM.API.Common.ExceptionHandlers;
using TinyCRM.Application.Common.Seeders;

namespace TinyCRM.API.Extensions;

public static class IApplicationBuilderExtension
{
    public static void UseExceptionHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

                var exception = exceptionHandlerPathFeature?.Error;

                if (exception != null)
                {
                    using var scope = app.ApplicationServices.CreateScope();

                    var exceptionHandler = scope.ServiceProvider.GetRequiredService<IExceptionHandler>();
                    await exceptionHandler.HandleAsync(context, exception);
                }
            });
        });
    }

    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var seeders = scope.ServiceProvider.GetRequiredService<IEnumerable<IDataSeeder>>();

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync();
        }
    }
}