using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

public class ContactService : BaseService<Contact, int, ContactDto, ContactCreateDto, ContactUpdateDto>, IContactService
{
    private readonly IAccountRepository _accountRepository;

    public ContactService(
        IAccountRepository accountRepository,
        IContactRepository contactRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(mapper, contactRepository, unitOfWork)
    {
        _accountRepository = accountRepository;
    }

    protected override async Task<bool> IsValidOnInsertAsync(ContactCreateDto contactCreateDto)
    {
        return await CheckAccountExistingAsync(contactCreateDto.AccountId);
    }

    protected override async Task<bool> IsValidOnUpdateAsync(Contact contact, ContactUpdateDto contactUpdateDto)
    {
        if (contact.AccountId != contactUpdateDto.AccountId)
        {
            return await CheckAccountExistingAsync(contactUpdateDto.AccountId);
        }

        return true;
    }

    private async Task<bool> CheckAccountExistingAsync(int? accountId)
    {
        if (accountId != null)
        {
            var isAccountExisting = await _accountRepository.AnyAsync(x => x.Id == accountId);
            if (!isAccountExisting)
            {
                throw new EntityNotFoundException(nameof(Account), accountId.Value);
            }
        }

        return true;
    }

    public async Task<AccountDto> GetAccountAsync(int id)
    {
        var contact = await _repository.FindAsync(x => x.Id == id, includeProps: nameof(Contact.Account), tracking: false);
        if (contact == null)
        {
            throw new EntityNotFoundException(nameof(Contact), id);
        }

        return _mapper.Map<AccountDto>(contact.Account);
    }
}