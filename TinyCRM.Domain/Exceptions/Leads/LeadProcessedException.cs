using TinyCRM.Domain.Common.Enums;

namespace TinyCRM.Domain.Exceptions.Leads;

public class LeadProcessedException : ResourceInvalidOperationException
{
    public LeadProcessedException(int id, LeadStatus currentStatus)
        : base($"Lead with id '{id}' has already been {currentStatus}!", ErrorCodes.ProcessedLead)
    {
    }
}