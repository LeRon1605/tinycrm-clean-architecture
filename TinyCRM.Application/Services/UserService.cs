using TinyCRM.Application.Common.Identity;
using TinyCRM.Application.Common.UnitOfWorks;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Application.Dtos.Users;
using TinyCRM.Application.Services.Abstracts;
using TinyCRM.Domain.Common.Constants;
using TinyCRM.Domain.Exceptions;
using TinyCRM.Domain.Exceptions.Resource;

namespace TinyCRM.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserManager _userManager;
    private readonly ICurrentUser _currentUser;

    public UserService(
        IUnitOfWork unitOfWork,
        IUserManager userManager,
        ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _currentUser = currentUser;
    }

    public async Task<PagedResultDto<UserDto>> GetPagedAsync(UserFilterAndPagingRequestDto filterParam)
    {
        var (data, total) = await _userManager.GetPagedAsync(filterParam);
        return new PagedResultDto<UserDto>()
        {
            Data = data,
            TotalPages = (int)Math.Ceiling(total * 1.0 / filterParam.Size)
        };
    }

    public async Task<UserDto> GetAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new ResourceNotFoundException("User", id);
        }

        return user;
    }

    public async Task<UserDto> CreateAsync(UserCreateDto userCreateDto)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var user = await _userManager.CreateAsync(userCreateDto, userCreateDto.Password);

            await _userManager.AddToRoleAsync(user.Id, AppRole.User);

            await _unitOfWork.CommitTransactionAsync();

            return user;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<UserDto> UpdateAsync(string id, UserUpdateDto userUpdateDto)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new ResourceNotFoundException("User", id);
        }

        IsValidOnEditUser(user);

        return await _userManager.UpdateAsync(id, userUpdateDto);
    }

    public async Task DeleteAsync(string id)
    {
        await _userManager.DeleteAsync(id);
    }

    private bool IsValidOnEditUser(UserDto user)
    {
        if (!_currentUser.IsInRole(AppRole.Admin) && _currentUser.Id != user.Id)
        {
            throw new ResourceAccessDeniedException("You don't have permission to edit this user!", ErrorCodes.ProfileForbidEdit);
        }

        return true;
    }
}