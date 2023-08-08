using TinyCRM.Application.Dtos.Users;
using TinyCRM.Application.UnitOfWorks;
using TinyCRM.Domain.Common.Constants;
using TinyCRM.Domain.Exceptions;

namespace TinyCRM.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IApplicationUserManager _userManager;
    private readonly ICurrentUser _currentUser;
    private readonly IPermissionGrantRepository _permissionGrantRepository;

    public UserService(
        IUnitOfWork unitOfWork,
        IApplicationUserManager userManager,
        ICurrentUser currentUser,
        IPermissionGrantRepository permissionGrantRepository)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _currentUser = currentUser;
        _permissionGrantRepository = permissionGrantRepository;
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
            var user = await _userManager.CreateAsync(userCreateDto);

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
        await _permissionGrantRepository.RemoveByUserAsync(id);
        await _unitOfWork.CommitAsync();
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