using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Application.Common.Identity;
using TinyCRM.Application.Dtos.Users;
using TinyCRM.Application.Repositories.Base;
using TinyCRM.Domain.Exceptions.Resource;
using TinyCRM.Infrastructure.Identity.Specifications;

namespace TinyCRM.Infrastructure.Identity.Services;

public class IdentityUserManager : IApplicationUserManager
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IReadOnlyRepository<ApplicationUser, string> _userRepository;
    private readonly IMapper _mapper;

    public IdentityUserManager(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IReadOnlyRepository<ApplicationUser, string> userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<UserDto>, int)> GetPagedAsync(UserFilterAndPagingRequestDto filterParam)
    {
        var userFilterSpecification = new UserFilterSpecification(filterParam.Page, filterParam.Size, filterParam.Name, filterParam.Sorting);

        var data = await _userRepository.GetPagedListAsync(userFilterSpecification);
        var total = await _userRepository.GetCountAsync(userFilterSpecification);

        return (_mapper.Map<IEnumerable<UserDto>>(data), total);
    }

    public async Task<UserDto> CreateAsync(UserCreateDto userCreateDto)
    {
        var user = _mapper.Map<ApplicationUser>(userCreateDto);
        var result = await _userManager.CreateAsync(user, userCreateDto.Password);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task AddToRoleAsync(string id, string name)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new ResourceNotFoundException("User", id);
        }

        var role = await _roleManager.FindByNameAsync(name);
        if (role == null)
        {
            throw new ResourceNotFoundException("Role", "name", name);
        }

        var result = await _userManager.AddToRoleAsync(user, name);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }
    }

    public async Task<UserDto> FindByIdAsync(string id)
    {
        var identityUser = await _userManager.FindByIdAsync(id);
        return _mapper.Map<UserDto>(identityUser);
    }

    public async Task<UserDto> UpdateAsync(string id, UserUpdateDto userUpdateDto)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new ResourceNotFoundException("User", id);
        }

        _mapper.Map(userUpdateDto, user);

        if (!string.IsNullOrWhiteSpace(userUpdateDto.Password))
        {
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userUpdateDto.Password);
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new ResourceNotFoundException("User", id);
        }

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }
    }
}