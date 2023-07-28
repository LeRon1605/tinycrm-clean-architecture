namespace Lab2.API.Exceptions;

public class AccountLockedOutException : BadRequestException
{
    public AccountLockedOutException() : base("This account has been locked out!", ErrorCodes.AccountLockedOut)
    {
    }
}