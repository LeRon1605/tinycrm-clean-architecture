using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lab2.API.Services;

public class UserService : BaseService<User, string, UserDto>, IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(
        UserManager<User> userManager,
        IMapper mapper,
        IRepository<User, string> repository,
        IUnitOfWork unitOfWork) : base(mapper, repository, unitOfWork)
    {
        _userManager = userManager;
    }

    public async Task<UserDto> CreateAsync(UserCreateDto userCreateDto)
    {
        var user = _mapper.Map<User>(userCreateDto);
        var createUserResult = await _userManager.CreateAsync(user, userCreateDto.Password);

        if (createUserResult.Succeeded)
        {
            var insertRoleResult = await _userManager.AddToRoleAsync(user, "User");
            if (insertRoleResult.Succeeded)
            {
                return _mapper.Map<UserDto>(user);
            }
            else
            {
                throw new IdentityException(insertRoleResult.Errors.First(x => !x.Code.Contains(nameof(IdentityUser.UserName))));
            }
        }
        else
        {
            throw new IdentityException(createUserResult.Errors.First(x => !x.Code.Contains(nameof(IdentityUser.UserName))));
        }
    }

    public async Task<UserDto> UpdateAsync(string id, UserUpdateDto entityUpdateDto)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new EntityNotFoundException(nameof(User), id);
        }

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
        else
        {
            throw new IdentityException(identityResult.Errors.First(x => !x.Code.Contains(nameof(IdentityUser.UserName))));
        }
    }
}