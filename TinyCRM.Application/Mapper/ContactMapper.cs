using TinyCRM.Application.Dtos.Contacts;

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