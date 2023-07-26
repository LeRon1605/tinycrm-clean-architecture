namespace Lab2.API.Exceptions;

public abstract class HttpException : Exception
{
    public int StatusCode { get; set; }
    public string ErrorCode { get; set; }

    public HttpException(string message) : base(message)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
        ErrorCode = string.Empty;
    }

    public HttpException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = string.Empty;
    }

    public HttpException(string message, int statusCode, string errorCode) : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }
}