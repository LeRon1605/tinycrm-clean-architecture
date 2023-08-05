using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Application.Dtos.Contacts;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Application.Services.Abstracts;
using TinyCRM.Domain.Common.Constants;

namespace TinyCRM.API.Controllers;

[ApiController]
[Authorize]
[Route("api/contacts")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResultDto<ContactDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllContactsAsync([FromQuery] ContactFilterAndPagingRequestDto contactFilterAndPagingRequestDto)
    {
        var contactDtos = await _contactService.GetPagedAsync(contactFilterAndPagingRequestDto);
        return Ok(contactDtos);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetDetailAsync))]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(int id)
    {
        var contactDto = await _contactService.GetAsync(id);
        return Ok(contactDto);
    }

    [HttpPost]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateContactAsync(ContactCreateDto contactCreateDto)
    {
        var contactDto = await _contactService.CreateAsync(contactCreateDto);
        return CreatedAtAction(nameof(GetDetailAsync), new { id = contactDto.Id }, contactDto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateContactAsync(int id, ContactUpdateDto contactUpdateDto)
    {
        var contactDto = await _contactService.UpdateAsync(id, contactUpdateDto);
        return Ok(contactDto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContactAsync(int id)
    {
        await _contactService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("account/{accountId}")]
    [ProducesResponseType(typeof(PagedResultDto<ContactDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContactsOfAccountAsync(int accountId, [FromQuery] ContactFilterAndPagingRequestDto contactFilterAndPagingRequestDto)
    {
        var contactDtos = await _contactService.GetByAccountAsync(accountId, contactFilterAndPagingRequestDto);
        return Ok(contactDtos);
    }
}