namespace Lab2.API.Exceptions;

public abstract class HttpException : Exception
{
    public int StatusCode { get; set; }

    public HttpException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}