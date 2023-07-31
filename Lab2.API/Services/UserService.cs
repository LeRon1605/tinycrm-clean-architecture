using AutoMapper;
using Lab2.API.Authorization;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Lab2.API.Services;

public class UserService : BaseService<User, string, UserDto>, IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUser _currentUser;

    public UserService(
        IAuthorizationService authorizationService,
        UserManager<User> userManager,
        ICurrentUser currentUser,
        IMapper mapper,
        IRepository<User, string> repository,
        IUnitOfWork unitOfWork) : base(mapper, repository, unitOfWork)
    {
        _authorizationService = authorizationService;
        _userManager = userManager;
        _currentUser = currentUser;
    }

    public async Task<UserDto> CreateAsync(UserCreateDto userCreateDto)
    {
        var user = _mapper.Map<User>(userCreateDto);

        await _unitOfWork.BeginTransactionAsync();

        var createUserResult = await _userManager.CreateAsync(user, userCreateDto.Password);
        if (!createUserResult.Succeeded)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw new IdentityException(createUserResult.Errors.First());
        }

        try
        {
            await _userManager.AddToRoleAsync(user, AppRole.User);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw new EntityNotFoundException("Role", nameof(IdentityRole.Name), AppRole.User);
        }

        await _unitOfWork.CommitTransactionAsync();
        
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateAsync(string id, UserUpdateDto entityUpdateDto)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new EntityNotFoundException(nameof(User), id);
        }

        await IsValidOnEditUser(user);

        _mapper.Map(entityUpdateDto, user);
        if (!string.IsNullOrWhiteSpace((entityUpdateDto.Password)))
        {
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, entityUpdateDto.Password);
        }

        var identityResult = await _userManager.UpdateAsync(user);
        if (identityResult.Succeeded)
        {
            return _mapper.Map<UserDto>(user);
        }

        throw new IdentityException(identityResult.Errors.First());
    }

    private async Task<bool> IsValidOnEditUser(User user)
    {
        var result = await _authorizationService.AuthorizeAsync(_currentUser.ClaimPrincipal, user, AppPolicy.EditProfile);

        if (!result.Succeeded)
        {
            throw new ForbiddenException("You don't have permission to edit user!", ErrorCodes.ProfileForbidEdit);
        }

        return true;
    }
}