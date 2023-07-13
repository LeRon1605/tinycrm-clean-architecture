using Lab2.API.Dtos;

namespace Lab2.API.Services;

public interface ILeadService
{
    Task<PagedResultDto<LeadDto>> GetAllAsync(LeadFilterAndPagingRequestDto leadFilterAndPagingRequestDto);
    Task<LeadDto> GetAsync(int id);
    Task<LeadDto> CreateAsync(LeadCreateDto leadCreateDto);
    Task<LeadDto> UpdateAsync(int id, LeadUpdateDto leadUpdateDto);
    Task DeleteAsync(int id);
    Task<LeadDto> GetLeadOfContactAsync(int contactId);
}
