namespace Lab2.API.Exceptions;

public class InvalidRemoveDealException : BadRequestException
{
    public InvalidRemoveDealException(int id) 
        : base($"Can not delete deal with id '{id}' which is on won or lost status!", ErrorCodes.DEAL_INVALID_REMOVE)
    {
    }
}