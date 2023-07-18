namespace Lab2.API.Exceptions;

public class BadRequestException : HttpException
{
    public BadRequestException(string message) : base(message, StatusCodes.Status400BadRequest)
    {

    }
}