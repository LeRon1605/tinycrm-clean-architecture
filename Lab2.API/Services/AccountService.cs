using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using System.Drawing;

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
        // Check if email exist
        if (await _accountRepository.AnyAsync(x => x.Email == accountCreateDto.Email))
        {
            // Throw exception if email existed
            throw new ConflictException($"Account with email '{accountCreateDto.Email}' has already existed.");
        }    

        // Create account from dto
        Account account = _mapper.Map<Account>(accountCreateDto);

        // Insert account to db
        _accountRepository.Insert(account);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<AccountDto>(account);
    }

    public async Task DeleteAsync(int id)
    {
        // Find account by Id
        Account account = await _accountRepository.FindAsync(x => x.Id == id);
        if (account == null)
        {
            // Throw exception if does not exist
            throw new NotFoundException($"Account with id '{id}' does not exist.");
        }

        // Delete account
        _accountRepository.Delete(account);
        await _unitOfWork.CommitAsync();
    }

    public async Task<AccountDto> GetAccountOfContactAsync(int contactId)
    {
        // Load contact with account
        Contact contact = await _contactRepository.FindDetailAsync(x => x.Id == contactId);
        if (contact == null)
        {
            // Throw exception if contact does not exist
            throw new NotFoundException($"Contact with id '{contactId}' does not exist.");
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
            Total = (int)Math.Ceiling(total * 1.0 / filterParam.Size)
        };
    }

    public async Task<AccountDto> GetAsync(int id)
    {
        // Fetch account by id
        Account account = await _accountRepository.FindAsync(x => x.Id == id);
        if (account == null)
        {
            // Throw exception if account does not exist
            throw new NotFoundException($"Account with id '{id}' does not exist.");
        }

        return _mapper.Map<AccountDto>(account);
    }

    public async Task<AccountDto> UpdateAsync(int id, AccountUpdateDto accountUpdateDto)
    {
        // Check if email exist
        if (await _accountRepository.AnyAsync(x => x.Email == accountUpdateDto.Email && x.Id != id))
        {
            // Throw exception if email existed
            throw new ConflictException($"Account with email '{accountUpdateDto.Email}' has already existed.");
        }

        // Fetch account by id
        Account account = await _accountRepository.FindAsync(x => x.Id == id);
        if (account == null)
        {
            // Throw exception if account does not exist
            throw new NotFoundException($"Account with id '{id}' does not exist.");
        }

        // Update account
        account.Name = accountUpdateDto.Name;
        account.Address = accountUpdateDto.Address;
        account.Phone = accountUpdateDto.Phone;
        account.Email = accountUpdateDto.Email;

        _accountRepository.Update(account);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<AccountDto>(account);
    }
}
