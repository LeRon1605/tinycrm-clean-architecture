using AutoMapper;
using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Mapper;

public class ContactMapper : Profile
{
    public ContactMapper()
    {
        CreateMap<Contact, ContactDto>();
        CreateMap<PagedResultDto<Contact>, PagedResultDto<ContactDto>>();
        CreateMap<ContactCreateDto, Contact>();
        CreateMap<ContactUpdateDto, Contact>();
    }
}