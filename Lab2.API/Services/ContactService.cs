using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

public class ContactService : IContactService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ContactService(
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

    public async Task<ContactDto> CreateAsync(ContactCreateDto contactCreateDto)
    {
        // Create contact from dto
        Contact contact = _mapper.Map<Contact>(contactCreateDto);

        // Check if account exist
        if (contact.AccountId != null && !(await _contactRepository.AnyAsync(x => x.Id == contact.AccountId)))
        {
            throw new NotFoundException($"Account with id '{contact.AccountId}' does not exist.");
        }

        // Insert contact to db
        _contactRepository.Insert(contact);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task DeleteAsync(int id)
    {
        Contact contact = await _contactRepository.FindAsync(x => x.Id == id);
        if (contact == null)
        {
            throw new NotFoundException($"Contact with id '{id}' does not exist.");
        }

        _contactRepository.Delete(contact);
        await _unitOfWork.CommitAsync();
    }

    public async Task<PagedResultDto<ContactDto>> GetAllAsync(ContactFilterAndPagingRequestDto filterParam)
    {
        var data = await _contactRepository.GetListAsync((filterParam.Page - 1) * filterParam.Size, filterParam.Size, x => x.Name.Contains(filterParam.Name));
        var total = await _contactRepository.CountAsync(x => x.Name.Contains(filterParam.Name));

        return new PagedResultDto<ContactDto>()
        {
            Data = _mapper.Map<List<ContactDto>>(data),
            Total = (int)Math.Ceiling(total * 1.0 / filterParam.Size)
        };
    }

    public async Task<ContactDto> GetAsync(int id)
    {
        Contact contact = await _contactRepository.FindAsync(x => x.Id == id);
        if (contact == null)
        {
            throw new NotFoundException($"Contact with id '{id}' does not exist.");
        }

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<IEnumerable<ContactDto>> GetContactsOfAccountAsync(int accountId)
    {
        // Load account with contact
        Account account = await _accountRepository.FindDetailAsync(x => x.Id == accountId);
        if (account == null)
        {
            // Throw exception if account does not exist
            throw new NotFoundException($"Account with id '{accountId}' does not exist.");
        }

        return _mapper.Map<IEnumerable<ContactDto>>(account.Contacts);
    }

    public async Task<ContactDto> UpdateAsync(int id, ContactUpdateDto contactUpdateDto)
    {
        // Fetch contact and check if contact exist
        Contact contact = await _contactRepository.FindAsync(x => x.Id == id);
        if (contact == null)
        {
            throw new NotFoundException($"Contact with id '{id}' does not exist.");
        }

        // Check if updated account exist
        if (contact.AccountId != null && !(await _contactRepository.AnyAsync(x => x.Id == contact.AccountId)))
        {
            throw new NotFoundException($"Account with id '{contact.AccountId}' does not exist.");
        }

        // Update contact
        contact.Name = contactUpdateDto.Name;
        contact.AccountId = contactUpdateDto.AccountId;
        contact.Phone = contactUpdateDto.Phone;
        contact.Email = contactUpdateDto.Email;

        _contactRepository.Update(contact);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ContactDto>(contact);
    }
}
