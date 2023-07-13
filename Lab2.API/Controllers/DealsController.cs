using Lab2.API.Dtos;
using Lab2.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

public class DealsController : BaseController
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

    [HttpPost]
    public async Task<IActionResult> CreateDeal(DealCreateDto dealCreateDto)
    {
        DealDto dealDto = await _dealService.CreateAsync(dealCreateDto);
        return CreatedAtAction(nameof(GetDetail), new { id = dealDto.Id }, dealDto);
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
        IEnumerable<DealLineDto> productDealDtos = await _productService.GetAllInDealAsync(id);
        return Ok(productDealDtos);
    }

    [HttpPost("{id}/products")]
    public async Task<IActionResult> AddLineToDeal(int id, DealLineCreateDto productDealCreateDto)
    {
        DealLineDto productDealDto = await _productService.AddToDealAsync(id, productDealCreateDto);
        return Ok(productDealDto);
    }

    [HttpPut("{id}/products/{lineId}")]
    public async Task<IActionResult> UpdateLineInDeal(int id, int lineId, DealLineUpdateDto productDealUpdateDto)
    {
        DealLineDto productDealDto = await _dealService.UpdateLineAsync(id, lineId, productDealUpdateDto);
        return Ok(productDealDto);
    }

    [HttpDelete("{id}/products/{lineId}")]
    public async Task<IActionResult> RemoveLineInDeal(int id, int lineId)
    {
        await _dealService.RemoveLineAsync(id, lineId);
        return NoContent();
    }
}
