using TinyCRM.Application.Dtos.Accounts;

namespace TinyCRM.API.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Accounts.View)]
    [ProducesResponseType(typeof(PagedResultDto<AccountDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAccountsAsync([FromQuery] AccountFilterAndPagingRequestDto accountFilterAndPagingRequestDto)
    {
        var accounts = await _accountService.GetPagedAsync(accountFilterAndPagingRequestDto);
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetDetailAsync))]
    [Authorize(Policy = Permissions.Accounts.View)]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(int id)
    {
        var account = await _accountService.GetAsync(id);
        return Ok(account);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Accounts.Create)]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAccountAsync(AccountCreateDto accountCreateDto)
    {
        var account = await _accountService.CreateAsync(accountCreateDto);
        return CreatedAtAction(nameof(GetDetailAsync), new { id = account.Id }, account);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.Accounts.Edit)]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateAccountAsync(int id, AccountUpdateDto accountUpdateDto)
    {
        var account = await _accountService.UpdateAsync(id, accountUpdateDto);
        return Ok(account);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Accounts.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAccountAsync(int id)
    {
        await _accountService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("contact/{contactId}")]
    [Authorize(Policy = Permissions.Accounts.View)]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountOfContactAsync(int contactId)
    {
        var account = await _accountService.GetByContactAsync(contactId);
        return Ok(account);
    }
}