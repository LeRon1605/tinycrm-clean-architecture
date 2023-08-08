using AutoMapper;
using TinyCRM.Application.Dtos.Roles;
using TinyCRM.Application.Repositories.Base;
using TinyCRM.Infrastructure.Identity.Specifications;

namespace TinyCRM.Infrastructure.Identity.Services;

public class IdentityRoleManager : IRoleManager
{
    private readonly IReadOnlyRepository<ApplicationRole, string> _roleRepository;
    private readonly IMapper _mapper;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityRoleManager(
        IReadOnlyRepository<ApplicationRole, string> roleRepository,
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _roleRepository = roleRepository;
        _roleManager = roleManager;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<(IEnumerable<RoleDto>, int)> GetPagedAsync(RoleFilterAndPagingRequestDto filterParam)
    {
        var specification = new RoleFilterSpecification(filterParam.Page, filterParam.Size, filterParam.Name, filterParam.Sorting);

        var data = await _roleRepository.GetPagedListAsync(specification);
        var total = await _roleRepository.GetCountAsync(specification);

        return (_mapper.Map<IEnumerable<RoleDto>>(data), total);
    }

    public async Task<RoleDto> FindByIdAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            throw new ResourceNotFoundException("Role", id);
        }

        return _mapper.Map<RoleDto>(role);
    }

    public async Task<RoleDto> CreateAsync(RoleCreateDto roleCreateDto)
    {
        var role = _mapper.Map<ApplicationRole>(roleCreateDto);
        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }

        return _mapper.Map<RoleDto>(role);
    }

    public async Task<RoleDto> UpdateAsync(string id, RoleUpdateDto roleUpdateDto)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            throw new ResourceNotFoundException("Role", id);
        }

        _mapper.Map(roleUpdateDto, role);

        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }

        return _mapper.Map<RoleDto>(role);
    }

    public async Task DeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            throw new ResourceNotFoundException("Role", id);
        }

        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }
    }

    public Task<bool> IsExistAsync(string role)
    {
        return _roleRepository.AnyAsync(x => x.Name == role);
    }

    public async Task<IEnumerable<string>> GetRolesForUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new ResourceNotFoundException("User", userId);
        }

        return await _userManager.GetRolesAsync(user);
    }
}