using TinyCRM.Domain.Exceptions.Resource;

namespace TinyCRM.Domain.Exceptions.Deals;

public class LineNotBelongToDealException : ResourceAccessDeniedException
{
    public LineNotBelongToDealException(int lineId, int dealId) : base($"Line with id '{lineId}' does not belong to deal with id '{dealId}'.", ErrorCodes.LineNotBelongToDeal)
    {
    }
}