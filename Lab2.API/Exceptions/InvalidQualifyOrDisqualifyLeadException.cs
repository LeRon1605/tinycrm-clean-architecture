using Lab2.Domain.Enums;

namespace Lab2.API.Exceptions;

public class InvalidQualifyOrDisqualifyLeadException : BadRequestException
{
    public InvalidQualifyOrDisqualifyLeadException(int id, LeadStatus currentStatus)
        : base($"Lead with id '{id}' has already been {currentStatus}!", ErrorCodes.LEAD_INVALID_QUALIFY_OR_DISQUALIFY)
    {
    }
}