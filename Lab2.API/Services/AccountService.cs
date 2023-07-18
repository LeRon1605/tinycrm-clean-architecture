using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

public class AccountService : BaseService<Account, AccountDto, AccountCreateDto, AccountUpdateDto>, IAccountService
{
    private readonly IContactRepository _contactRepository;

    public AccountService(
        IAccountRepository accountRepository,
        IContactRepository contactRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(mapper, accountRepository, unitOfWork)
    {
        _contactRepository = contactRepository;
    }

    protected override async Task<bool> IsValidOnInsertAsync(AccountCreateDto accountCreateDto)
    {
        return await CheckDuplicateAccountEmailAsync(accountCreateDto.Email);
    }

    protected override async Task<bool> IsValidOnUpdateAsync(Account account, AccountUpdateDto accountUpdateDto)
    {
        if (account.Email != accountUpdateDto.Email) 
        {
            await CheckDuplicateAccountEmailAsync(accountUpdateDto.Email);
        }

        return true;
    }

    public async Task<AccountDto> GetAccountOfContactAsync(int contactId)
    {
        // Load contact with account
        var contact = await _contactRepository.FindAsync(x => x.Id == contactId, includeProps: nameof(Contact.Account), tracking: false);
        if (contact == null)
        {
            throw new EntityNotFoundException("Contact", contactId);
        }

        return Mapper.Map<AccountDto>(contact.Account);
    }

    private async Task<bool> CheckDuplicateAccountEmailAsync(string email)
    {
        var isEmailExisting = await Repository.AnyAsync(x => x.Email == email);
        if (isEmailExisting)
        {
            throw new EntityConflictException("Account", "email", email);
        }

        return true;
    }
}