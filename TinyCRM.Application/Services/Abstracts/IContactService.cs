using TinyCRM.Application.Dtos.Contacts;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Services.Abstracts;

public interface IContactService : IService<Contact, int, ContactDto, ContactCreateDto, ContactUpdateDto>
{
    Task<PagedResultDto<ContactDto>> GetByAccountAsync(int accountId, ContactFilterAndPagingRequestDto accountFilterAndPagingRequestDto);
}