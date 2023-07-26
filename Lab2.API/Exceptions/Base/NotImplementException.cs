namespace Lab2.API.Exceptions;

public class NotImplementException : HttpException
{
    public NotImplementException(string message) : base(message, StatusCodes.Status501NotImplemented)
    {
    }

    public NotImplementException(string message, string errorCode) : base(message, StatusCodes.Status501NotImplemented, errorCode)
    {
    }
}