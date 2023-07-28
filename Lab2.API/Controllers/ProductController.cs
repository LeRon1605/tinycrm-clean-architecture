using Lab2.API.Authorization;
using Lab2.API.Dtos;
using Lab2.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResultDto<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllProductsAsync([FromQuery] ProductFilterAndPagingRequestDto productFilterAndPagingRequestDto)
    {
        var productDtos = await _productService.GetPagedAsync(productFilterAndPagingRequestDto);
        return Ok(productDtos);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetDetailAsync))]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(int id)
    {
        var productDto = await _productService.GetAsync(id);
        return Ok(productDto);
    }

    [HttpPost]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateProductAsync(ProductCreateDto productCreateDto)
    {
        var productDto = await _productService.CreateAsync(productCreateDto);
        return CreatedAtAction(nameof(GetDetailAsync), new { id = productDto.Id }, productDto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto)
    {
        var productDto = await _productService.UpdateAsync(id, productUpdateDto);
        return Ok(productDto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}