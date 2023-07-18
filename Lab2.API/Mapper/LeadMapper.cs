using AutoMapper;
using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Mapper;

public class LeadMapper : Profile
{
    public LeadMapper()
    {
        CreateMap<Lead, LeadDto>();
        CreateMap<LeadCreateDto, Lead>();
        CreateMap<LeadUpdateDto, Lead>();
    }
}