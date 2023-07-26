using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

public class AccountService : BaseService<Account, int, AccountDto, AccountCreateDto, AccountUpdateDto>, IAccountService
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
        return await CheckValidAccountAsync(accountCreateDto.Email, accountCreateDto.Phone);
    }

    protected override async Task<bool> IsValidOnUpdateAsync(Account account, AccountUpdateDto accountUpdateDto)
    {
        if (account.Email != accountUpdateDto.Email || account.Phone != accountUpdateDto.Phone)
        {
            await CheckValidAccountAsync(accountUpdateDto.Email, accountUpdateDto.Phone, account.Id);
        }

        return true;
    }

    public async Task<AccountDto> GetAccountOfContactAsync(int contactId)
    {
        // Load contact with account
        var contact = await _contactRepository.FindAsync(x => x.Id == contactId, includeProps: nameof(Contact.Account), tracking: false);
        if (contact == null)
        {
            throw new EntityNotFoundException(nameof(Contact), contactId);
        }

        return _mapper.Map<AccountDto>(contact.Account);
    }

    private async Task<bool> CheckValidAccountAsync(string email, string phone, int? id = null)
    {
        var account = await _repository.FindAsync(x => (x.Email == email || x.Phone == phone) && (id == null || id.Value == x.Id));
        if (account != null)
        {
            if (account.Email == email)
            {
                throw new AccountAlreadyExistException(nameof(Account.Email), email);
            }

            if (account.Phone == phone)
            {
                throw new AccountAlreadyExistException(nameof(Account.Phone), phone);
            }
        }

        return true;
    }
}