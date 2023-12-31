﻿using System.Text;
using TinyCRM.API.Dtos;
using TinyCRM.Domain.Exceptions;
using TinyCRM.Domain.Exceptions.Resource;

namespace TinyCRM.API.Common.ExceptionHandlers;

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

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = GetResponseStatusCode(exception);

        var errorResponse = GetErrorResponse(exception);
        await context.Response.WriteAsJsonAsync(errorResponse);
    }

    private void LogException(HttpContext context, Exception exception)
    {
        _logger.LogError(BuildLoggingMessage(context, exception), exception);
    }

    private string BuildLoggingMessage(HttpContext context, Exception exception)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder = stringBuilder.Append($"[{context.Request.Method}] {DateTime.Now} {context.Request.Path} {context.Connection.RemoteIpAddress}: {exception.Message}{Environment.NewLine}");
        return stringBuilder.ToString();
    }

    private ErrorResponse GetErrorResponse(Exception exception)
    {
        return new ErrorResponse()
        {
            Code = exception switch
            {
                CoreException coreException => coreException.ErrorCode,
                _ => ErrorCodes.SystemError
            },
            Message = exception switch
            {
                CoreException coreException => coreException.Message,
                _ => _env.IsDevelopment() ? exception.Message : "Something went wrong, please try again!"
            }
        };
    }

    private int GetResponseStatusCode(Exception exception)
    {
        return exception switch
        {
            ResourceNotFoundException => StatusCodes.Status404NotFound,
            ResourceAccessDeniedException => StatusCodes.Status403Forbidden,
            ResourceAlreadyExistException => StatusCodes.Status409Conflict,
            ResourceInvalidOperationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}