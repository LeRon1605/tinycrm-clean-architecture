using TinyCRM.Application.Dtos.Accounts;

namespace TinyCRM.Application.Services.Interfaces;

public interface IAccountService : IService<Account, int, AccountDto, AccountCreateDto, AccountUpdateDto>
{
    Task<AccountDto> GetByContactAsync(int id);
}