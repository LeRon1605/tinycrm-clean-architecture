using Lab2.API.Dtos;
using Lab2.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

public class DealsController : ApiController
{
    private readonly IDealService _dealService;
    private readonly IProductService _productService;
    public DealsController(IDealService dealService, IProductService productService)
    {
        _dealService = dealService;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDeals([FromQuery] DealFilterAndPagingRequestDto dealFilterAndPagingRequestDto)
    {
        PagedResultDto<DealDto> deals = await _dealService.GetAllAsync(dealFilterAndPagingRequestDto);
        return Ok(deals);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail(int id)
    {
        DealDto dealDto = await _dealService.GetAsync(id);
        return Ok(dealDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDeal(int id, DealUpdateDto dealUpdateDto)
    {
        DealDto dealDto = await _dealService.UpdateAsync(id, dealUpdateDto);
        return Ok(dealDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDeal(int id)
    {
        await _dealService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetProductsInDeal(int id)
    {
        IEnumerable<DealLineDto> productDealDtos = await _dealService.GetProductsInDealAsync(id);
        return Ok(productDealDtos);
    }

    [HttpPost("{id}/products")]
    public async Task<IActionResult> AddLineToDeal(int id, DealLineCreateDto productDealCreateDto)
    {
        DealLineDto productDealDto = await _dealService.AddLineAsync(id, productDealCreateDto);
        return Ok(productDealDto);
    }
}
