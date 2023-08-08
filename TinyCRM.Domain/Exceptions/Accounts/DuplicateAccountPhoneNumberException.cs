using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Exceptions.Accounts;

public class DuplicateAccountPhoneNumberException : ResourceAlreadyExistException
{
    public DuplicateAccountPhoneNumberException(string phone) : base(nameof(Account), nameof(Account.Phone), phone, ErrorCodes.DuplicateAccountPhoneNumber)
    {
    }
}