using AutoMapper;
using TinyCRM.Application.Common.UnitOfWorks;
using TinyCRM.Application.Dtos.Accounts;
using TinyCRM.Application.Repositories;
using TinyCRM.Application.Services.Abstracts;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Exceptions.Accounts;
using TinyCRM.Domain.Exceptions.Resource;

namespace TinyCRM.Application.Services;

public class AccountService : BaseService<Account, int, AccountDto, AccountCreateDto, AccountUpdateDto>, IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IContactRepository _contactRepository;

    public AccountService(
        IAccountRepository accountRepository,
        IContactRepository contactRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(mapper, accountRepository, unitOfWork)
    {
        _accountRepository = accountRepository;
        _contactRepository = contactRepository;
    }

    protected override async Task<bool> IsValidOnInsertAsync(AccountCreateDto accountCreateDto)
    {
        return await CheckValidEmailAsync(accountCreateDto.Email) && await CheckValidPhoneAsync(accountCreateDto.Phone);
    }

    protected override async Task<bool> IsValidOnUpdateAsync(Account account, AccountUpdateDto accountUpdateDto)
    {
        if (account.Email != accountUpdateDto.Email)
        {
            await CheckValidEmailAsync(accountUpdateDto.Email);
        }

        if (account.Phone != accountUpdateDto.Phone)
        {
            await CheckValidPhoneAsync(accountUpdateDto.Phone);
        }

        return true;
    }

    private async Task<bool> CheckValidEmailAsync(string email)
    {
        if (await _accountRepository.IsEmailExistingAsync(email))
        {
            throw new DuplicateAccountEmailException(email);
        }

        return true;
    }

    private async Task<bool> CheckValidPhoneAsync(string phone)
    {
        if (await _accountRepository.IsPhoneExistingAsync(phone))
        {
            throw new DuplicateAccountPhoneNumberException(phone);
        }

        return true;
    }

    public async Task<AccountDto> GetByContactAsync(int id)
    {
        var contact = await _contactRepository.FindByIdAsync(id, includeProps: nameof(Contact.Account), tracking: false);
        if (contact == null)
        {
            throw new ResourceNotFoundException(nameof(Contact), id);
        }

        return _mapper.Map<AccountDto>(contact.Account);
    }
}