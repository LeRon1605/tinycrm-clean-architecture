using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Application.Dtos.Users;

namespace TinyCRM.Application.Services.Abstracts;

public interface IUserService
{
    Task<PagedResultDto<UserDto>> GetPagedAsync(UserFilterAndPagingRequestDto userFilterAndPagingRequestDto);

    Task<UserDto> GetAsync(string id);

    Task<UserDto> CreateAsync(UserCreateDto userCreateDto);

    Task<UserDto> UpdateAsync(string id, UserUpdateDto userUpdateDto);

    Task DeleteAsync(string id);
}