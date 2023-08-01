using Lab2.API.Authorization;
using Lab2.API.Dtos;
using Lab2.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

[ApiController]
[Authorize]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
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

    [HttpGet("contact/{contactId}")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountOfContactAsync(int contactId)
    {
        var accountDto = await _accountService.GetByContactAsync(contactId);
        return Ok(accountDto);
    }
}