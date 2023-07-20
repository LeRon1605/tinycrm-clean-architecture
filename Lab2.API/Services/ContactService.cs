using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.API.Extensions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

public class ContactService : BaseService<Contact, ContactDto, ContactCreateDto, ContactUpdateDto>, IContactService
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

    public async Task<PagedResultDto<ContactDto>> GetContactsOfAccountAsync(int accountId, ContactFilterAndPagingRequestDto filterParam)
    {
        // Check if account exist
        await CheckAccountExistingAsync(accountId);

        return await GetPagedAsync(skip: (filterParam.Page - 1) * filterParam.Size,
                                   take: filterParam.Size,
                                   expression: filterParam.ToExpression().JoinWith(x => x.AccountId == accountId),
                                   sorting: filterParam.BuildSortingParam());
    }

    private async Task<bool> CheckAccountExistingAsync(int? accountId)
    {
        if (accountId != null)
        {
            var isAccountExisting = await _accountRepository.AnyAsync(x => x.Id == accountId);
            if (!isAccountExisting)
            {
                throw new EntityNotFoundException("Account", accountId.Value);
            }
        }

        return true;
    }
}