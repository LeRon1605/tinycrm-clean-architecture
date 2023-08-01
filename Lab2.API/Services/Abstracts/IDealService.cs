using Lab2.API.Dtos;
using Lab2.API.Dtos.Deals;
using Lab2.Domain.Entities;

namespace Lab2.API.Services;

public interface IDealService : IService<Deal, int, DealDto, DealCreateDto, DealUpdateDto>
{
    Task<DealStatisticDto> GetStatisticAsync();
    
    Task<DealLineDto> AddLineAsync(int id, DealLineCreateDto dealLineCreateDto);

    Task<DealLineDto> UpdateLineAsync(int id, int dealLineId, DealLineUpdateDto dealLineUpdateDto);

    Task RemoveLineAsync(int id, int dealLineId);

    Task<PagedResultDto<DealLineDto>> GetLinesAsync(int id, DealLineFilterAndPagingRequestDto filterParam);
}