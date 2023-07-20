namespace Lab2.API.Exceptions;

public class ConflictException : HttpException
{
    public ConflictException(string message) : base(message, StatusCodes.Status409Conflict)
    {
    }
}