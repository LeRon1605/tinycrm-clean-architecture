namespace TinyCRM.Domain.Exceptions;

public class CoreException : Exception
{
    public string ErrorCode { get; set; }

    public CoreException(string message) : base(message)
    {
    }

    public CoreException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }
}