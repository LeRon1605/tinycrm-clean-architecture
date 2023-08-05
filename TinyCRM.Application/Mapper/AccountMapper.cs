using TinyCRM.Application.Dtos.Accounts;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Mapper;

public class AccountMapper : TinyCrmMapper
{
    public AccountMapper()
    {
        CreateMap<Account, BasicAccountDto>();

        CreateMap<Account, AccountDto>();
        CreateMap<PagedResultDto<Account>, PagedResultDto<AccountDto>>();
        CreateMap<AccountCreateDto, Account>();
        CreateMap<AccountUpdateDto, Account>();
    }
}