namespace Lab2.API.Exceptions;

public class BadRequestException : HttpException
{
    public BadRequestException(string message) : base(message, StatusCodes.Status400BadRequest)
    {
    }

    public BadRequestException(string message, string errorCode) : base(message, StatusCodes.Status400BadRequest, errorCode)
    {
    }
}