using Lab2.API.Dtos;
using System.Text;

namespace Lab2.API.Exceptions;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandler(ILogger<ExceptionHandler> logger, IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async Task HandleAsync(HttpContext context, Exception exception)
    {
        LogException(context, exception);

        var errorResponse = GetErrorResponse(exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            HttpException => ((HttpException)exception).StatusCode,
            _ => StatusCodes.Status500InternalServerError
        };
        await context.Response.WriteAsJsonAsync(errorResponse);
    }

    private void LogException(HttpContext context, Exception exception)
    {
        if (exception is not HttpException)
        {
            _logger.LogError(BuildLoggingMessage(context, exception));
        }
        else
        {
            _logger.LogWarning(BuildLoggingMessage(context, exception));
        }
    }

    private string BuildLoggingMessage(HttpContext context, Exception exception)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder = stringBuilder.Append($"[{context.Request.Method}] {DateTime.Now} {context.Request.Path} {context.Connection.RemoteIpAddress}: {exception.Message}{Environment.NewLine}");

        if (_env.IsDevelopment())
        {
            stringBuilder = stringBuilder.Append(exception.StackTrace);
        }

        return stringBuilder.ToString();
    }

    private ErrorResponse GetErrorResponse(Exception exception)
    {
        return new ErrorResponse()
        {
            Code = exception switch
            {
                HttpException => ((HttpException)exception).ErrorCode,
                _ => ErrorCodes.INTERNAL_SERVER_ERROR
            },
            Message = exception switch
            {
                HttpException => ((HttpException)exception).Message,
                _ => _env.IsDevelopment() ? exception.Message : "Something went wrong, please try again!"
            }
        };
    }
}