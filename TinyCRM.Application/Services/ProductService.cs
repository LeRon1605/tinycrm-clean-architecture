using AutoMapper;
using TinyCRM.Application.Common.UnitOfWorks;
using TinyCRM.Application.Dtos.Products;
using TinyCRM.Application.Repositories;
using TinyCRM.Application.Services.Abstracts;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Exceptions.Products;

namespace TinyCRM.Application.Services;

public class ProductService : BaseService<Product, int, ProductDto, ProductCreateDto, ProductUpdateDto>, IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(mapper, productRepository, unitOfWork)
    {
        _productRepository = productRepository;
    }

    protected override async Task<bool> IsValidOnInsertAsync(ProductCreateDto productCreateDto)
    {
        return await CheckDuplicateProductCode(productCreateDto.Code);
    }

    protected override async Task<bool> IsValidOnUpdateAsync(Product product, ProductUpdateDto productUpdateDto)
    {
        if (product.Code != productUpdateDto.Code)
        {
            return await CheckDuplicateProductCode(productUpdateDto.Code);
        }

        return true;
    }

    private async Task<bool> CheckDuplicateProductCode(string code)
    {
        var isProductCodeExisting = await _productRepository.IsCodeExistingAsync(code);
        if (isProductCodeExisting)
        {
            throw new DuplicateProductCodeException(code);
        }

        return true;
    }
}