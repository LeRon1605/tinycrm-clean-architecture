using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

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
            throw new ProductCodeAlreadyExist(code);
        }

        return true;
    }
}