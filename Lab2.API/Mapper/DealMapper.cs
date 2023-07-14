using AutoMapper;
using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Mapper;

public class DealMapper : Profile
{
    public DealMapper()
    {
        CreateMap<Deal, DealDto>()
            .ForMember(dest => dest.ActualRevenue, opt => opt.MapFrom(src => src.Lines.Sum(x => x.Quantity * x.PricePerUnit)));
        CreateMap<DealCreateDto, Deal>();
        CreateMap<DealUpdateDto, Deal>();

        CreateMap<DealLine, DealLineDto>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Quantity * src.PricePerUnit));
        CreateMap<DealLineCreateDto, DealLine>();
    }
}
