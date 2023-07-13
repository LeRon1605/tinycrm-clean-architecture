using Lab2.API.Dtos;

namespace Lab2.API.Services;

public interface IDealService
{
    Task<PagedResultDto<DealDto>> GetAllAsync(DealFilterAndPagingRequestDto dealFilterAndPagingRequestDto);
    Task<DealDto> GetAsync(int id);
    Task<DealDto> CreateAsync(DealCreateDto dealCreateDto);
    Task<DealDto> UpdateAsync(int id, DealUpdateDto dealUpdateDto);
    Task DeleteAsync(int id);
    Task RemoveLineAsync(int id, int lineId);
    Task<DealLineDto> UpdateLineAsync(int id, int lineId, DealLineUpdateDto productDealUpdateDto);
}
