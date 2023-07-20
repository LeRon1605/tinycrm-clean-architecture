using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using System.Text;
using System.Text.RegularExpressions;

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
            context.Request.EnableBuffering();
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            // Log exception
            if (exception is not HttpException)
            {
                _logger.LogError(await BuildLoggingMessage(context, exception));
            }
            else
            {
                _logger.LogWarning(await BuildLoggingMessage(context, exception));
            }

            // Handle exception
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorResponse = new ErrorResponse()
        {
            Message = exception switch
            {
                HttpException => ((HttpException)exception).Message,
                _ => "Something went wrong, please try again!"
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

    private async Task<string> BuildLoggingMessage(HttpContext context, Exception exception)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder = stringBuilder.Append($"[{context.Request.Method}] {DateTime.Now} {context.Request.Path} {context.Connection.RemoteIpAddress}{Environment.NewLine}");
        stringBuilder = stringBuilder.Append($"Error message: {exception.Message}{Environment.NewLine}");
        stringBuilder = stringBuilder.Append($"Query string: {context.Request.QueryString}{Environment.NewLine}");
        stringBuilder = stringBuilder.Append($"Request Body: {await GetRequestBodyAsync(context)}");

        return stringBuilder.ToString();
    }

    private async Task<string> GetRequestBodyAsync(HttpContext context)
    {
        context.Request.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(context.Request.Body);
        var requestBody = await reader.ReadToEndAsync();

        return "{" + new Regex(@"(\n |\r |\n|{|})+").Replace(requestBody, "") + " }";
    }
}