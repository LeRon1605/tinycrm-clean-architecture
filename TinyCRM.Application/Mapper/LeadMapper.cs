using TinyCRM.Application.Dtos.Leads;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Mapper;

public class LeadMapper : TinyCrmMapper
{
    public LeadMapper()
    {
        CreateMap<Lead, LeadDto>();
        CreateMap<PagedResultDto<Lead>, PagedResultDto<LeadDto>>();
        CreateMap<LeadCreateDto, Lead>();
        CreateMap<LeadUpdateDto, Lead>();
    }
}