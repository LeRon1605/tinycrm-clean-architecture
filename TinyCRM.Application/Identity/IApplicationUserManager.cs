using TinyCRM.Application.Dtos.Users;

namespace TinyCRM.Application.Identity;

public interface IApplicationUserManager
{
    Task<(IEnumerable<UserDto>, int)> GetPagedAsync(UserFilterAndPagingRequestDto userFilterAndPagingRequestDto);

    Task<UserDto> CreateAsync(UserCreateDto userCreateDto);

    Task AddToRoleAsync(string id, string name);

    Task<UserDto> FindByIdAsync(string id);

    Task<UserDto> UpdateAsync(string id, UserUpdateDto userUpdateDto);

    Task DeleteAsync(string id);

    Task<bool> IsExistAsync(string id);
}