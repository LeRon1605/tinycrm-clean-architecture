using Lab2.Domain.Enums;

namespace Lab2.API.Exceptions;

public class LineNotBelongToDealException : ForbiddenException
{
    public LineNotBelongToDealException(int lineId, int dealId) : base($"Line with id '{lineId}' does not belong to deal with id '{dealId}'.", ErrorCodes.LineNotBelongToDeal)
    {
    }
}