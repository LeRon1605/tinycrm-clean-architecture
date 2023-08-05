using TinyCRM.Application.Dtos.Deals;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Services.Abstracts;

public interface IDealService : IService<Deal, int, DealDto, DealCreateDto, DealUpdateDto>
{
    Task<DealStatisticDto> GetStatisticAsync();

    Task<DealLineDto> AddLineAsync(int id, DealLineCreateDto dealLineCreateDto);

    Task<DealLineDto> UpdateLineAsync(int id, int dealLineId, DealLineUpdateDto dealLineUpdateDto);

    Task RemoveLineAsync(int id, int dealLineId);

    Task<PagedResultDto<DealLineDto>> GetLinesAsync(int id, DealLineFilterAndPagingRequestDto filterParam);
}