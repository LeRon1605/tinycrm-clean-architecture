using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Services;

public interface IAccountService : IService<Account, int, AccountDto, AccountCreateDto, AccountUpdateDto>
{
    Task<PagedResultDto<ContactDto>> GetContactsAsync(int id, ContactFilterAndPagingRequestDto accountFilterAndPagingRequestDto);

    Task<PagedResultDto<LeadDto>> GetLeadsAsync(int id, LeadFilterAndPagingRequestDto filterParam);
}