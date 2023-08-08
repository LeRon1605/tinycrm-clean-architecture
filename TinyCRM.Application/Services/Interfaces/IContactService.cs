using TinyCRM.Application.Dtos.Contacts;

namespace TinyCRM.Application.Services.Interfaces;

public interface IContactService : IService<Contact, int, ContactDto, ContactCreateDto, ContactUpdateDto>
{
    Task<PagedResultDto<ContactDto>> GetByAccountAsync(int accountId, ContactFilterAndPagingRequestDto accountFilterAndPagingRequestDto);
}