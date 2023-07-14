using Lab2.API.Dtos;

namespace Lab2.API.Services;

public interface IDealService
{
    Task<PagedResultDto<DealDto>> GetAllAsync(DealFilterAndPagingRequestDto dealFilterAndPagingRequestDto);
    Task<DealDto> GetAsync(int id);
    Task<DealDto> UpdateAsync(int id, DealUpdateDto dealUpdateDto);
    Task DeleteAsync(int id);
    Task<DealLineDto> AddLineAsync(int id, DealLineCreateDto productDealCreateDto);
    Task<IEnumerable<DealLineDto>> GetProductsInDealAsync(int id);
}
