namespace Lab2.API.Exceptions;

public class ForbiddenException : HttpException
{
    public ForbiddenException(string message) : base(message, StatusCodes.Status403Forbidden)
    {
    }
}