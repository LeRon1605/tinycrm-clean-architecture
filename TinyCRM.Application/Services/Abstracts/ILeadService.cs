using TinyCRM.Application.Dtos.Deals;
using TinyCRM.Application.Dtos.Leads;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Services.Abstracts;

public interface ILeadService : IService<Lead, int, LeadDto, LeadCreateDto, LeadUpdateDto>
{
    Task<DealDto> QualifyAsync(int id);

    Task<LeadDto> DisqualifyAsync(int id, DisqualifiedLeadCreateDto disqualifiedLeadCreateDto);

    Task<LeadStatisticDto> GetStatisticAsync();

    Task<PagedResultDto<LeadDto>> GetByAccountAsync(int accountId, LeadFilterAndPagingRequestDto filterParam);
}