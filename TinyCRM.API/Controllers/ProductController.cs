using TinyCRM.Application.Dtos.Products;

namespace TinyCRM.API.Controllers;

[ApiController]
[Authorize]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Products.View)]
    [ProducesResponseType(typeof(PagedResultDto<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllProductsAsync([FromQuery] ProductFilterAndPagingRequestDto productFilterAndPagingRequestDto)
    {
        var products = await _productService.GetPagedAsync(productFilterAndPagingRequestDto);
        return Ok(products);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetDetailAsync))]
    [Authorize(Policy = Permissions.Products.View)]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(int id)
    {
        var product = await _productService.GetAsync(id);
        return Ok(product);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Products.Create)]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateProductAsync(ProductCreateDto productCreateDto)
    {
        var product = await _productService.CreateAsync(productCreateDto);
        return CreatedAtAction(nameof(GetDetailAsync), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.Products.Edit)]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto)
    {
        var product = await _productService.UpdateAsync(id, productUpdateDto);
        return Ok(product);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Products.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}