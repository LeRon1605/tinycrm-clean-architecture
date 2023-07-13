using Lab2.API.Dtos;

namespace Lab2.API.Services;

public interface IContactService
{
    Task<PagedResultDto<ContactDto>> GetAllAsync(ContactFilterAndPagingRequestDto contactFilterAndPagingRequestDto);
    Task<ContactDto> GetAsync(int id);
    Task<ContactDto> CreateAsync(ContactCreateDto contactCreateDto);
    Task<ContactDto> UpdateAsync(int id, ContactUpdateDto contactUpdateDto);
    Task DeleteAsync(int id);
    Task<IEnumerable<ContactDto>> GetContactsOfAccountAsync(int accountId);
}
