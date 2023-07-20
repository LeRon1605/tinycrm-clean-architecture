using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Services;

public interface ILineService : IService<DealLine, DealLineDto, DealLineCreateDto, DealLineUpdateDto>
{
    Task<PagedResultDto<DealLineDto>> GetProductsInDealAsync(int dealId, DealLineFilterAndPagingRequestDto filterParam);
}