using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Services;

public interface IAccountService : IService<Account, int, AccountDto, AccountCreateDto, AccountUpdateDto>
{
    Task<AccountDto> GetByContactAsync(int id);
}