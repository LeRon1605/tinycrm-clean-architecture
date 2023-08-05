using TinyCRM.Domain.Exceptions.Resource;

namespace TinyCRM.Infrastructure.Identity.Exceptions;

public class InvalidCredentialException : ResourceNotFoundException
{
    public InvalidCredentialException() : base("Account with provided information does not exist!")
    {
        ErrorCode = IdentityErrorCodes.InvalidCredential;
    }
}