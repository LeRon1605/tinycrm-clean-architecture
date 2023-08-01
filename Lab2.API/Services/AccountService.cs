using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Domain.Specifications;

namespace Lab2.API.Services;

public class AccountService : BaseService<Account, int, AccountDto, AccountCreateDto, AccountUpdateDto>, IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IContactRepository _contactRepository;
    private readonly ILeadRepository _leadRepository;

    public AccountService(
        IAccountRepository accountRepository,
        IContactRepository contactRepository,
        ILeadRepository leadRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(mapper, accountRepository, unitOfWork)
    {
        _accountRepository = accountRepository;
        _contactRepository = contactRepository;
        _leadRepository = leadRepository;
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

    public async Task<PagedResultDto<ContactDto>> GetContactsAsync(int id, ContactFilterAndPagingRequestDto filterParam)
    {
        // 1. Check account existing
        await CheckExistingAsync(id);

        // 2. Get contacts
        var getPagedContactForAccountSpecification = filterParam.ToSpecification().And(new GetContactForAccountSpecification(id));

        var pagedData = await _contactRepository.GetPagedListAsync(getPagedContactForAccountSpecification);
        var total = await _contactRepository.GetCountAsync(getPagedContactForAccountSpecification);

        return new PagedResultDto<ContactDto>()
        {
            Data = _mapper.Map<IEnumerable<ContactDto>>(pagedData),
            TotalPages = (int)Math.Ceiling(total * 1.0 / filterParam.Size)
        };
    }

    public async Task<PagedResultDto<LeadDto>> GetLeadsAsync(int id, LeadFilterAndPagingRequestDto filterParam)
    {
        // 1. Check account existing
        await CheckExistingAsync(id);

        // 2. Get leads
        var getPagedLeadForAccountSpecification = filterParam.ToSpecification().And(new GetLeadForAccountSpecification(id));

        var pagedData = await _leadRepository.GetPagedListAsync(getPagedLeadForAccountSpecification);
        var total = await _leadRepository.GetCountAsync(getPagedLeadForAccountSpecification);

        return new PagedResultDto<LeadDto>()
        {
            Data = _mapper.Map<IEnumerable<LeadDto>>(pagedData),
            TotalPages = (int)Math.Ceiling(total * 1.0 / filterParam.Size)
        };
    }
}