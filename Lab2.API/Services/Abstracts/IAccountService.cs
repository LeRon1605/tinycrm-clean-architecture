using Lab2.API.Dtos;

namespace Lab2.API.Services;

public interface IAccountService
{
    Task<PagedResultDto<AccountDto>> GetAllAsync(AccountFilterAndPagingRequestDto accountFilterAndPagingRequestDto);
    Task<AccountDto> GetAsync(int id);
    Task<AccountDto> CreateAsync(AccountCreateDto accountCreateDto);
    Task<AccountDto> UpdateAsync(int id, AccountUpdateDto accountUpdateDto);
    Task DeleteAsync(int id);
    Task<AccountDto> GetAccountOfContactAsync(int contactId);
}
