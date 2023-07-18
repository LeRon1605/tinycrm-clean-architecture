using Lab2.API.Dtos;
using Lab2.API.Filters;
using Lab2.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

public class ProductController : ApiController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [SortQueryConstraint(Fields = "Code, Name, Price")]
    [ProducesResponseType(typeof(PagedResultDto<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllProducts([FromQuery] ProductFilterAndPagingRequestDto productFilterAndPagingRequestDto)
    {
        var productDtos = await _productService.GetPagedAsync(productFilterAndPagingRequestDto);
        return Ok(productDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetail(int id)
    {
        var productDto = await _productService.GetAsync(id);
        return Ok(productDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateProduct(ProductCreateDto productCreateDto)
    {
        var productDto = await _productService.CreateAsync(productCreateDto);
        return CreatedAtAction(nameof(GetDetail), new { id = productDto.Id }, productDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
    {
        var productDto = await _productService.UpdateAsync(id, productUpdateDto);
        return Ok(productDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}