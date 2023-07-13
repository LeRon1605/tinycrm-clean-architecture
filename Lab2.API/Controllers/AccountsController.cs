using Lab2.API.Dtos;
using Lab2.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

public class AccountsController : BaseController
{
    private readonly IAccountService _accountService;
    private readonly IContactService _contactService;
    public AccountsController(IAccountService accountService, IContactService contactService)
    {
        _accountService = accountService;
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAccounts([FromQuery] AccountFilterAndPagingRequestDto accountFilterAndPagingRequestDto)
    {
        PagedResultDto<AccountDto> accounts = await _accountService.GetAllAsync(accountFilterAndPagingRequestDto);
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail(int id)
    {
        AccountDto accountDto = await _accountService.GetAsync(id);
        if (accountDto == null)
        {
            return NotFound();
        }
        return Ok(accountDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount(AccountCreateDto accountCreateDto)
    {
        AccountDto accountDto = await _accountService.CreateAsync(accountCreateDto);
        return CreatedAtAction(nameof(GetDetail), new { id = accountDto.Id }, accountDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAccount(int id, AccountUpdateDto accountUpdateDto)
    {
        AccountDto accountDto = await _accountService.UpdateAsync(id, accountUpdateDto);
        return Ok(accountDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        await _accountService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/contacts")]
    public async Task<IActionResult> GetContactsOfAccount(int id)
    {
        IEnumerable<ContactDto> contacts = await _contactService.GetContactsOfAccountAsync(id);
        return Ok(contacts);
    }
}
