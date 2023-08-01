using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Services;

public interface IContactService : IService<Contact, int, ContactDto, ContactCreateDto, ContactUpdateDto>
{
    Task<PagedResultDto<ContactDto>> GetByAccountAsync(int accountId, ContactFilterAndPagingRequestDto accountFilterAndPagingRequestDto);
}