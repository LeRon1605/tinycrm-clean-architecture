using Lab2.API.Dtos;
using Lab2.API.Filters;
using Lab2.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

public class ContactController : ApiController
{
    private readonly IContactService _contactService;
    private readonly IAccountService _accountService;
    public ContactController(IContactService contactService, IAccountService accountService)
    {
        _contactService = contactService;
        _accountService = accountService;
    }

    [HttpGet]
    [SortQueryConstraint(Fields = "Name, Email")]
    [ProducesResponseType(typeof(PagedResultDto<ContactDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllContacts([FromQuery] ContactFilterAndPagingRequestDto contactFilterAndPagingRequestDto)
    {
        var contactDtos = await _contactService.GetPagedAsync(contactFilterAndPagingRequestDto);
        return Ok(contactDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetail(int id)
    {
        var contactDto = await _contactService.GetAsync(id);
        return Ok(contactDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateContact(ContactCreateDto contactCreateDto)
    {
        var contactDto = await _contactService.CreateAsync(contactCreateDto);
        return CreatedAtAction(nameof(GetDetail), new { id = contactDto.Id }, contactDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateContact(int id, ContactUpdateDto contactUpdateDto)
    {
        var contactDto = await _contactService.UpdateAsync(id, contactUpdateDto);
        return Ok(contactDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContact(int id)
    {
        await _contactService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/account")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountOfContact(int id)
    {
        var accountDto = await _accountService.GetAccountOfContactAsync(id);
        return Ok(accountDto);
    }
}
