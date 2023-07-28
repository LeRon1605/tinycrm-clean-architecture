using Lab2.API.Authorization;
using Lab2.API.Dtos;
using Lab2.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IContactService _contactService;
    private readonly ILeadService _leadService;

    public AccountController(
        IAccountService accountService,
        IContactService contactService,
        ILeadService leadService)
    {
        _accountService = accountService;
        _contactService = contactService;
        _leadService = leadService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResultDto<AccountDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAccountsAsync([FromQuery] AccountFilterAndPagingRequestDto accountFilterAndPagingRequestDto)
    {
        var accountDtos = await _accountService.GetPagedAsync(accountFilterAndPagingRequestDto);
        return Ok(accountDtos);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetDetailAsync))]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(int id)
    {
        var accountDto = await _accountService.GetAsync(id);
        return Ok(accountDto);
    }

    [HttpPost]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAccountAsync(AccountCreateDto accountCreateDto)
    {
        var accountDto = await _accountService.CreateAsync(accountCreateDto);
        return CreatedAtAction(nameof(GetDetailAsync), new { id = accountDto.Id }, accountDto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateAccountAsync(int id, AccountUpdateDto accountUpdateDto)
    {
        var accountDto = await _accountService.UpdateAsync(id, accountUpdateDto);
        return Ok(accountDto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAccountAsync(int id)
    {
        await _accountService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/contacts")]
    [ProducesResponseType(typeof(PagedResultDto<ContactDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContactsOfAccountAsync(int id, [FromQuery] ContactFilterAndPagingRequestDto contactFilterAndPagingRequestDto)
    {
        var contactDtos = await _contactService.GetContactsOfAccountAsync(id, contactFilterAndPagingRequestDto);
        return Ok(contactDtos);
    }

    [HttpGet("{id}/leads")]
    [ProducesResponseType(typeof(IEnumerable<LeadDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLeadsOfAccountAsync(int id, [FromQuery] LeadFilterAndPagingRequestDto filterParam)
    {
        var leadDtos = await _leadService.GetLeadsOfAccountAsync(id, filterParam);
        return Ok(leadDtos);
    }
}