namespace Lab2.API.Exceptions;

public class NotFoundException : HttpException
{
    public NotFoundException(string message) : base(message, StatusCodes.Status404NotFound)
    {
    }
}