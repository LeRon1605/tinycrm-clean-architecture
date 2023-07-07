using Lab2.API.Dtos;
using Lab2.API.Exceptions;

namespace Lab2.API.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try 
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            var errorResponse = new ErrorResponse()
            {
                Message = exception switch {
                    HttpException => ((HttpException)exception).Message,
                    _ => "Internal server error."
                }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                HttpException => ((HttpException)exception).StatusCode,
                _ => StatusCodes.Status500InternalServerError
            };
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
