using TinyCRM.Application.Dtos.Accounts;

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