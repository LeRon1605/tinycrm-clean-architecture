using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

public class ProductService : BaseService<Product, ProductDto, ProductCreateDto, ProductUpdateDto>, IProductService
{
    public ProductService(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(mapper, productRepository, unitOfWork)
    {
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
        var isProductCodeExisting = await _repository.AnyAsync(x => x.Code == code);
        if (isProductCodeExisting)
        {
            throw new ProductCodeAlreadyExist(code);
        }

        return true;
    }
}