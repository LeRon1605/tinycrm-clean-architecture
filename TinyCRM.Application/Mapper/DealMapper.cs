using TinyCRM.Application.Dtos.Deals;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Mapper;

public class DealMapper : TinyCrmMapper
{
    public DealMapper()
    {
        CreateMap<Deal, DealDto>()
            .ForMember(dest => dest.ActualRevenue, opt => opt.MapFrom(src => src.Lines.Sum(x => x.Quantity * x.PricePerUnit)));
        CreateMap<PagedResultDto<Deal>, PagedResultDto<DealDto>>();
        CreateMap<DealCreateDto, Deal>();
        CreateMap<DealUpdateDto, Deal>();

        CreateMap<DealLine, DealLineDto>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Quantity * src.PricePerUnit));
        CreateMap<PagedResultDto<DealLine>, PagedResultDto<DealLineDto>>();
        CreateMap<DealLineCreateDto, DealLine>();
    }
}