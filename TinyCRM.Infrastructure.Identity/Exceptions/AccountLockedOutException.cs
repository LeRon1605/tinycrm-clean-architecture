namespace TinyCRM.Infrastructure.Identity.Exceptions;

public class AccountLockedOutException : ResourceAccessDeniedException
{
    public AccountLockedOutException() : base("This account has been locked out!", IdentityErrorCodes.AccountLockedOut)
    {
    }
}