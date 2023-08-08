using TinyCRM.Application.Dtos.Leads;

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