using Lab2.API.Dtos;

namespace Lab2.API.Services;

public interface IProductService
{
    Task<PagedResultDto<ProductDto>> GetAllAsync(ProductFilterAndPagingRequestDto productFilterAndPagingRequestDto);
    Task<ProductDto> GetAsync(int id);
    Task<ProductDto> CreateAsync(ProductCreateDto productCreateDto);
    Task<ProductDto> UpdateAsync(int id, ProductUpdateDto productUpdateDto);
    Task DeleteAsync(int id);
    Task<IEnumerable<DealLineDto>> GetAllInDealAsync(int dealId);
    Task<DealLineDto> AddToDealAsync(int dealId, DealLineCreateDto productDealCreateDto);
}
