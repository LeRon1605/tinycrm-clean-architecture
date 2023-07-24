namespace Lab2.API.Exceptions;

public class InvalidRemoveLeadException : BadRequestException
{
    public InvalidRemoveLeadException(int id) 
        : base($"Can not delete lead with id '{id}' which is on qualified or disqualified status!", ErrorCodes.LeadInvalidRemove)
    {
    }
}