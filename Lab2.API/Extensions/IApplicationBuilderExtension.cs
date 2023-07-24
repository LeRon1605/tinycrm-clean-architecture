using Lab2.API.Exceptions;
using Lab2.Domain;
using Microsoft.AspNetCore.Diagnostics;

namespace Lab2.API.Extensions;

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
        var dataContributor = scope.ServiceProvider.GetRequiredService<DataContributor>();

        await dataContributor.SeedAsync();
    }
}