using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

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
            throw new AccountAlreadyExistException(nameof(Account.Email), email);
        }

        return true;
    }

    private async Task<bool> CheckValidPhoneAsync(string phone)
    {
        if (await _accountRepository.IsPhoneExistingAsync(phone))
        {
            throw new AccountAlreadyExistException(nameof(Account.Phone), phone);
        }

        return true;
    }

    public async Task<AccountDto> GetByContactAsync(int id)
    {
        var contact = await _contactRepository.FindByIdAsync(id, includeProps: nameof(Contact.Account), tracking: false);
        if (contact == null)
        {
            throw new EntityNotFoundException(nameof(Contact), id);
        }

        return _mapper.Map<AccountDto>(contact.Account);
    }
}