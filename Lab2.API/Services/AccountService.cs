using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AccountService(
        IAccountRepository accountRepository, 
        IContactRepository contactRepository,
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _accountRepository = accountRepository;
        _contactRepository = contactRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;   
    }

    public async Task<AccountDto> CreateAsync(AccountCreateDto accountCreateDto)
    {
        // Create account from dto
        Account account = _mapper.Map<Account>(accountCreateDto);

        // Insert account to db
        _accountRepository.Insert(account);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<AccountDto>(account);
    }

    public async Task DeleteAsync(int id)
    {
        Account account = await _accountRepository.FindAsync(x => x.Id == id);
        if (account == null)
        {
            throw new NotFoundException($"Account with id '{id}' does not exist.");
        }

        _accountRepository.Delete(account);
        await _unitOfWork.CommitAsync();
    }

    public async Task<AccountDto> GetAccountOfContactAsync(int contactId)
    {
        Contact contact = await _contactRepository.FindDetailAsync(x => x.Id == contactId);
        if (contact == null)
        {
            throw new NotFoundException($"Account with id '{contactId}' does not exist.");
        }

        return _mapper.Map<AccountDto>(contact.Account);
    }

    public async Task<PagedResultDto<AccountDto>> GetAllAsync(AccountFilterAndPagingRequestDto filterParam)
    {
        var data = await _accountRepository.GetListAsync((filterParam.Page - 1) * filterParam.Size, filterParam.Size, x => x.Name.Contains(filterParam.Name));
        var total = await _accountRepository.CountAsync(x => x.Name.Contains(filterParam.Name));

        return new PagedResultDto<AccountDto>()
        {
            Data = _mapper.Map<List<AccountDto>>(data),
            Total = total
        };
    }

    public async Task<AccountDto> GetAsync(int id)
    {
        Account account = await _accountRepository.FindAsync(x => x.Id == id);
        if (account == null)
        {
            throw new NotFoundException($"Account with id '{id}' does not exist.");
        }

        return _mapper.Map<AccountDto>(account);
    }

    public async Task<AccountDto> UpdateAsync(int id, AccountUpdateDto accountUpdateDto)
    {
        Account account = await _accountRepository.FindAsync(x => x.Id == id);
        if (account == null)
        {
            throw new NotFoundException($"Account with id '{id}' does not exist.");
        }

        account.Name = accountUpdateDto.Name;
        account.Address = accountUpdateDto.Address;
        account.Phone = accountUpdateDto.Phone;
        account.Email = accountUpdateDto.Email;

        _accountRepository.Update(account);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<AccountDto>(account);
    }
}
