using Lab2.API.Dtos;
using Lab2.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

public class ProductsController : BaseController
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;   
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts([FromQuery] ProductFilterAndPagingRequestDto productFilterAndPagingRequestDto)
    {
        PagedResultDto<ProductDto> products = await _productService.GetAllAsync(productFilterAndPagingRequestDto);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail(int id)
    {
        ProductDto productDto = await _productService.GetAsync(id);
        return Ok(productDto);
    }

    [HttpPost]    
    public async Task<IActionResult> CreateProduct(ProductCreateDto productCreateDto)
    {
        ProductDto productDto = await _productService.CreateAsync(productCreateDto);
        return CreatedAtAction(nameof(GetDetail), new { id = productDto.Id }, productDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
    {
        ProductDto productDto = await _productService.UpdateAsync(id, productUpdateDto);
        return Ok(productDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}
