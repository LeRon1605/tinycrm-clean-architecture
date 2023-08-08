using TinyCRM.Application.Dtos.Contacts;

namespace TinyCRM.API.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Contacts.View)]
    [ProducesResponseType(typeof(PagedResultDto<ContactDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllContactsAsync([FromQuery] ContactFilterAndPagingRequestDto contactFilterAndPagingRequestDto)
    {
        var contacts = await _contactService.GetPagedAsync(contactFilterAndPagingRequestDto);
        return Ok(contacts);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.Contacts.View)]
    [ActionName(nameof(GetDetailAsync))]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(int id)
    {
        var contact = await _contactService.GetAsync(id);
        return Ok(contact);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Contacts.Create)]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateContactAsync(ContactCreateDto contactCreateDto)
    {
        var contact = await _contactService.CreateAsync(contactCreateDto);
        return CreatedAtAction(nameof(GetDetailAsync), new { id = contact.Id }, contact);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.Contacts.Edit)]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateContactAsync(int id, ContactUpdateDto contactUpdateDto)
    {
        var contact = await _contactService.UpdateAsync(id, contactUpdateDto);
        return Ok(contact);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Contacts.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContactAsync(int id)
    {
        await _contactService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("account/{accountId}")]
    [Authorize(Policy = Permissions.Accounts.View)]
    [ProducesResponseType(typeof(PagedResultDto<ContactDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContactsOfAccountAsync(int accountId, [FromQuery] ContactFilterAndPagingRequestDto contactFilterAndPagingRequestDto)
    {
        var contacts = await _contactService.GetByAccountAsync(accountId, contactFilterAndPagingRequestDto);
        return Ok(contacts);
    }
}