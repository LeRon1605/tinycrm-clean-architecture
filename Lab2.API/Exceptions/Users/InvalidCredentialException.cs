namespace Lab2.API.Exceptions;

public class InvalidCredentialException : NotFoundException
{
    public InvalidCredentialException() : base("Account with provided information does not exist!", ErrorCodes.InvalidCredential)
    {
    }
}