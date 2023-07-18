using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.API.Filters;
using Lab2.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

public class AccountController : ApiController
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
    [SortQueryConstraint(Fields = "Name, Email")]
    [ProducesResponseType(typeof(PagedResultDto<AccountDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAccounts([FromQuery] AccountFilterAndPagingRequestDto accountFilterAndPagingRequestDto)
    {
        var accountDtos = await _accountService.GetPagedAsync(accountFilterAndPagingRequestDto);
        return Ok(accountDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetail(int id)
    {
        var accountDto = await _accountService.GetAsync(id);
        return Ok(accountDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAccount(AccountCreateDto accountCreateDto)
    {
        var accountDto = await _accountService.CreateAsync(accountCreateDto);
        return CreatedAtAction(nameof(GetDetail), new { id = accountDto.Id }, accountDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateAccount(int id, AccountUpdateDto accountUpdateDto)
    {
        var accountDto = await _accountService.UpdateAsync(id, accountUpdateDto);
        return Ok(accountDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        await _accountService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/contacts")]
    [SortQueryConstraint(Fields = "Name, Email")]
    [ProducesResponseType(typeof(PagedResultDto<ContactDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContactsOfAccount(int id, [FromQuery] ContactFilterAndPagingRequestDto contactFilterAndPagingRequestDto)
    {
        var contactDtos = await _contactService.GetContactsOfAccountAsync(id, contactFilterAndPagingRequestDto);
        return Ok(contactDtos);
    }

    [HttpGet("{id}/leads")]
    [SortQueryConstraint(Fields = "Title, EstimatedRevenue")]
    [ProducesResponseType(typeof(IEnumerable<LeadDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLeadsOfAccount(int id, [FromQuery] LeadFilterAndPagingRequestDto filterParam)
    {
        var leadDtos = await _leadService.GetLeadsOfAccountAsync(id, filterParam);
        return Ok(leadDtos);
    }
}