using TinyCRM.Application.Dtos.Permissions;

namespace TinyCRM.Application.Mapper;

public class PermissionMapper : TinyCrmMapper
{
    public PermissionMapper()
    {
        CreateMap<PermissionGrant, PermissionDto>();
        CreateMap<PermissionContent, PermissionDto>();
    }
}