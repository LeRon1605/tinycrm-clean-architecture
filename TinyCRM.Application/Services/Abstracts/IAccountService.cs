using TinyCRM.Application.Dtos.Accounts;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Services.Abstracts;

public interface IAccountService : IService<Account, int, AccountDto, AccountCreateDto, AccountUpdateDto>
{
    Task<AccountDto> GetByContactAsync(int id);
}