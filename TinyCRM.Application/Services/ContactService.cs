using TinyCRM.Application.Dtos.Contacts;
using TinyCRM.Application.UnitOfWorks;
using TinyCRM.Domain.Specifications.Contacts;

namespace TinyCRM.Application.Services;

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

    public async Task<PagedResultDto<ContactDto>> GetByAccountAsync(int accountId, ContactFilterAndPagingRequestDto filterParam)
    {
        // 1. Check account existing
        await CheckAccountExistingAsync(accountId);

        // 2. Get contacts
        var getPagedContactForAccountSpecification = filterParam.ToSpecification().And(new ContactForAccountSpecification(accountId));
        return await GetPagedAsync(getPagedContactForAccountSpecification);
    }

    private async Task<bool> CheckAccountExistingAsync(int? accountId)
    {
        if (accountId != null)
        {
            var isAccountExisting = await _accountRepository.IsExistingAsync(accountId.Value);
            if (!isAccountExisting)
            {
                throw new ResourceNotFoundException(nameof(Account), accountId.Value);
            }
        }

        return true;
    }
}