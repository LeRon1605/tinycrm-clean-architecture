using Lab2.API.Dtos;
using Lab2.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

public class ContactsController : ApiController
{
    private readonly IContactService _contactService;
    private readonly IAccountService _accountService;
    public ContactsController(IContactService contactService, IAccountService accountService)
    {
        _contactService = contactService;
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContacts([FromQuery] ContactFilterAndPagingRequestDto contactFilterAndPagingRequestDto)
    {
        PagedResultDto<ContactDto> contacts = await _contactService.GetAllAsync(contactFilterAndPagingRequestDto);
        return Ok(contacts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail(int id)
    {
        ContactDto contactDto = await _contactService.GetAsync(id);
        return Ok(contactDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(ContactCreateDto contactCreateDto)
    {
        ContactDto contactDto = await _contactService.CreateAsync(contactCreateDto);
        return CreatedAtAction(nameof(GetDetail), new { id = contactDto.Id }, contactDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact(int id, ContactUpdateDto contactUpdateDto)
    {
        ContactDto contactDto = await _contactService.UpdateAsync(id, contactUpdateDto);
        return Ok(contactDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        await _contactService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/account")]
    public async Task<IActionResult> GetAccountOfContact(int id)
    {
        AccountDto accountDto = await _accountService.GetAccountOfContactAsync(id);
        return Ok(accountDto);
    }
}
