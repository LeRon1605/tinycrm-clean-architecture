namespace Lab2.API.Exceptions;

public class ConflictException : HttpException
{
    public ConflictException(string message) : base(message, StatusCodes.Status409Conflict)
    {
    }

    public ConflictException(string message, string errorCode) : base(message, StatusCodes.Status409Conflict, errorCode)
    {
    }
}