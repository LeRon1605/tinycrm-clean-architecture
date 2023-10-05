using TinyCRM.Application.Dtos.Roles;

namespace TinyCRM.Infrastructure.Identity.Mapper;

public class ApplicationRoleMapper : IdentityMapper
{
    public ApplicationRoleMapper()
    {
        CreateMap<RoleCreateDto, ApplicationRole>();
        CreateMap<RoleUpdateDto, ApplicationRole>();
        CreateMap<ApplicationRole, RoleDto>();
    }
}