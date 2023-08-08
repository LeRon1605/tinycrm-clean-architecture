namespace TinyCRM.Domain.Exceptions.Permissions;

public class PermissionNotGrantedException : ResourceNotFoundException
{
    public PermissionNotGrantedException(int id) : base($"Permission with id '{id}' has not been granted!")
    {
        ErrorCode = ErrorCodes.PermissionNotGranted;
    }
}