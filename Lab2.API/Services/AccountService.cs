using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.API.Extensions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

public class AccountService : BaseService<Account, int, AccountDto, AccountCreateDto, AccountUpdateDto>, IAccountService
{
    private readonly IContactRepository _contactRepository;
    private readonly ILeadRepository _leadRepository;

    public AccountService(
        IAccountRepository accountRepository,
        IContactRepository contactRepository,
        ILeadRepository leadRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(mapper, accountRepository, unitOfWork)
    {
        _contactRepository = contactRepository;
        _leadRepository = leadRepository;
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

    public async Task<PagedResultDto<ContactDto>> GetContactsAsync(int id, ContactFilterAndPagingRequestDto filterParam)
    {
        // 1. Check account existing
        await CheckExistingAsync(id);

        // 2. Get contacts 
        var pagedResult = await _contactRepository.GetPagedResultAsync(skip: (filterParam.Page - 1) * filterParam.Size,
                                                                        take: filterParam.Size,
                                                                        expression: filterParam.ToExpression().JoinWith(x => x.AccountId == id),
                                                                        filterParam.BuildSortingParam(),
                                                                        tracking: false);
        return _mapper.Map<PagedResultDto<ContactDto>>(pagedResult);
    }

    public async Task<PagedResultDto<LeadDto>> GetLeadsAsync(int id, LeadFilterAndPagingRequestDto filterParam)
    {
        // 1. Check account existing
        await CheckExistingAsync(id);

        // 2. Get leads 
        var pagedResult = await _leadRepository.GetPagedResultAsync(skip: (filterParam.Page - 1) * filterParam.Size,
                                                                    take: filterParam.Size,
                                                                    expression: filterParam.ToExpression().JoinWith(x => x.CustomerId == id),
                                                                    filterParam.BuildSortingParam(),
                                                                    tracking: false);
        return _mapper.Map<PagedResultDto<LeadDto>>(pagedResult);
    }
}