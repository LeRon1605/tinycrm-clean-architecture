using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Exceptions.Accounts;

public class DuplicateAccountEmailException : ResourceAlreadyExistException
{
    public DuplicateAccountEmailException(string phone) : base(nameof(Account), nameof(Account.Phone), phone, ErrorCodes.DuplicateAccountEmail)
    {
    }
}