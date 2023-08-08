namespace TinyCRM.Domain.Exceptions.Permissions;

public class PermissionAlreadyGrantedException : ResourceAlreadyExistException
{
    public PermissionAlreadyGrantedException(int id) : base($"Permission with id '{id}' has already been granted!", ErrorCodes.PermissionAlreadyGranted)
    {
    }
}