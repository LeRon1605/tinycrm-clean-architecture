namespace TinyCRM.Domain.Exceptions.Roles;

public class BasicRoleAccessDeniedException : ResourceAccessDeniedException
{
    public BasicRoleAccessDeniedException() : base("Can not update or delete role 'Admin'!", ErrorCodes.BasicRoleAccessDenied)
    {
    }
}