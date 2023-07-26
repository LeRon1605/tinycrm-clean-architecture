using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Services;

public interface ILeadService : IService<Lead, int, LeadDto, LeadCreateDto, LeadUpdateDto>
{
    Task<PagedResultDto<LeadDto>> GetLeadsOfAccountAsync(int accountId, LeadFilterAndPagingRequestDto filterParam);

    Task<DealDto> QualifyAsync(int id);

    Task<LeadDto> DisqualifyAsync(int id, DisqualifiedLeadCreateDto disqualifiedLeadCreateDto);

    Task<LeadStatisticDto> GetStatisticAsync();
}