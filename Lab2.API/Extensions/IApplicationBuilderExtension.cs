using Lab2.API.Middlewares;

namespace Lab2.API.Extensions;

public static class IApplicationBuilderExtension
{
    public static void UseExceptionHandling(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
