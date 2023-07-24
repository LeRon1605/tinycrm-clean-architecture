namespace Lab2.API.Exceptions;

public interface IExceptionHandler
{
    Task HandleAsync(HttpContext context, Exception exception);
}
