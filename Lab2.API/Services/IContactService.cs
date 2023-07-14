using AutoMapper;
using Lab2.API.Dtos;
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

    public Task<ContactDto> CreateAsync(ContactCreateDto contactCreateDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResultDto<ContactDto>> GetAllAsync(ContactFilterAndPagingRequestDto contactFilterAndPagingRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<ContactDto> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ContactDto>> GetContactsOfAccountAsync(int accountId)
    {
        List<Contact> contacts = await _contactRepository.GetListAsync(x => x.AccountId == accountId);
        return _mapper.Map<IEnumerable<ContactDto>>(contacts);
    }

    public Task<ContactDto> UpdateAsync(int id, ContactUpdateDto contactUpdateDto)
    {
        throw new NotImplementedException();
    }
}
