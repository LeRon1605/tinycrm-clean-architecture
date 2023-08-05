using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Exceptions.Resource;

namespace TinyCRM.Domain.Exceptions.Accounts;

public class DuplicateAccountEmailException : ResourceAlreadyExistException
{
    public DuplicateAccountEmailException(string phone) : base(nameof(Account), nameof(Account.Phone), phone, ErrorCodes.DuplicateAccountEmail)
    {
    }
}