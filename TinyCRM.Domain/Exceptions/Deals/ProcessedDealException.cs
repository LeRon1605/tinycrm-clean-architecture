using TinyCRM.Domain.Common.Enums;
using TinyCRM.Domain.Exceptions.Resource;

namespace TinyCRM.Domain.Exceptions.Deals;

public class ProcessedDealException : ResourceInvalidOperationException
{
    public ProcessedDealException(int id, DealStatus currentStatus)
        : base($"Deal with id '{id}' has already been {currentStatus}!", ErrorCodes.ProcessedDeal)
    {
    }
}