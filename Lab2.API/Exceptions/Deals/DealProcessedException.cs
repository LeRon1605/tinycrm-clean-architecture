using Lab2.Domain.Enums;

namespace Lab2.API.Exceptions;

public class DealProcessedException : BadRequestException
{
    public DealProcessedException(int id, DealStatus status) : base($"Deal with id '{id}' has been {status}.", ErrorCodes.DealProcessed)
    {
    }
}