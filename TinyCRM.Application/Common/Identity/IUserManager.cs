using TinyCRM.Application.Dtos.Users;

namespace TinyCRM.Application.Common.Identity;

public interface IUserManager
{
    Task<(IEnumerable<UserDto>, int)> GetPagedAsync(UserFilterAndPagingRequestDto userFilterAndPagingRequestDto);

    Task<UserDto> CreateAsync(UserCreateDto userCreateDto, string password);

    Task AddToRoleAsync(string id, string name);

    Task<UserDto> FindByIdAsync(string id);

    Task<UserDto> UpdateAsync(string id, UserUpdateDto userUpdateDto);

    Task DeleteAsync(string id);
}