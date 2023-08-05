using TinyCRM.Application.Dtos.Users;

namespace TinyCRM.Infrastructure.Identity.Mapper;

public class ApplicationUserMapper : IdentityMapper
{
    public ApplicationUserMapper()
    {
        CreateMap<ApplicationUser, UserDto>();
        CreateMap<UserCreateDto, ApplicationUser>();
        CreateMap<UserUpdateDto, ApplicationUser>();
    }
}