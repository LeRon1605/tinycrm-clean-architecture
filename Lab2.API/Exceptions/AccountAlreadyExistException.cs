using Lab2.Domain.Entities;

namespace Lab2.API.Exceptions;

public class AccountAlreadyExistException : EntityConflictException
{
    public AccountAlreadyExistException(string column, string value) : base(nameof(Account), column, value)
    {
        if (string.Equals(column, nameof(Account.Email), StringComparison.OrdinalIgnoreCase))
        {
            ErrorCode = ErrorCodes.ACCOUNT_EMAIL_ALREADY_EXISTS;
        }
        else if (string.Equals(column, nameof(Account.Phone), StringComparison.OrdinalIgnoreCase))
        {
            ErrorCode = ErrorCodes.ACCOUNT_PHONE_ALREADY_EXISTS;
        }
    }
}