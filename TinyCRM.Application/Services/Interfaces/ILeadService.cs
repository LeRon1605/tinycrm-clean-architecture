using TinyCRM.Application.Dtos.Deals;
using TinyCRM.Application.Dtos.Leads;

namespace TinyCRM.Application.Services.Interfaces;

public interface ILeadService : IService<Lead, int, LeadDto, LeadCreateDto, LeadUpdateDto>
{
    Task<DealDto> QualifyAsync(int id);

    Task<LeadDto> DisqualifyAsync(int id, DisqualifiedLeadCreateDto disqualifiedLeadCreateDto);

    Task<LeadStatisticDto> GetStatisticAsync();

    Task<PagedResultDto<LeadDto>> GetByAccountAsync(int accountId, LeadFilterAndPagingRequestDto filterParam);
}