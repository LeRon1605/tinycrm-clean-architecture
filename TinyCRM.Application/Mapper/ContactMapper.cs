using TinyCRM.Application.Dtos.Contacts;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Mapper;

public class ContactMapper : TinyCrmMapper
{
    public ContactMapper()
    {
        CreateMap<Contact, ContactDto>();
        CreateMap<PagedResultDto<Contact>, PagedResultDto<ContactDto>>();
        CreateMap<ContactCreateDto, Contact>();
        CreateMap<ContactUpdateDto, Contact>();
    }
}