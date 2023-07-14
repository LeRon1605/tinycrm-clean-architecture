using AutoMapper;
using EntityFramework.Exceptions.Common;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(
        IProductRepository productRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> CreateAsync(ProductCreateDto productCreateDto)
    {
        // Create product from dto
        Product product = _mapper.Map<Product>(productCreateDto);
        try
        {
            // Insert product to db
            await _productRepository.InsertAsync(product);
            await _unitOfWork.CommitAsync();
        } 
        catch (UniqueConstraintException)
        {
            // Catch unique product code constraint exception, throw it to outer layer
            throw new ConflictException($"Product with code '{productCreateDto.Code}' has already existed.");
        }
        
        return _mapper.Map<ProductDto>(product);
    }

    public async Task DeleteAsync(int id)
    {
        // Fetch product from database
        Product product = await _productRepository.FindAsync(x => x.Id == id);
        if (product == null)
        {
            // Throw error if product does not exist
            throw new NotFoundException($"Product with id '{id}' does not exist");
        }

        // Delete product from db
        _productRepository.Delete(product);
        await _unitOfWork.CommitAsync();
    }

    public async Task<PagedResultDto<ProductDto>> GetAllAsync(ProductFilterAndPagingRequestDto filterParam)
    {
        // Perform filter and paging product by name and product type
        var data = await _productRepository.GetListAsync(
                                                skip: (filterParam.Page - 1) * filterParam.Size, 
                                                take: filterParam.Size, 
                                                x => x.Name.Contains(filterParam.Name) && 
                                                    (filterParam.Type == null || x.Type == filterParam.Type.Value) 
                                            );
        var total = await _productRepository.CountAsync(x => x.Name.Contains(filterParam.Name) && (filterParam.Type == null || x.Type == filterParam.Type));

        return new PagedResultDto<ProductDto>()
        {
            Data = _mapper.Map<List<ProductDto>>(data),
            Total = (int)Math.Ceiling(total * 1.0 / filterParam.Size)
        };
    }

    public async Task<ProductDto> GetAsync(int id)
    {
        Product product = await _productRepository.FindAsync(x => x.Id == id);
        if (product == null)
        {
            throw new NotFoundException($"Product with id '{id}' does not exist");
        }

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> UpdateAsync(int id, ProductUpdateDto productUpdateDto)
    {
        // Fetch product from db
        Product product = await _productRepository.FindAsync(x => x.Id == id);
        if (product == null)
        {
            // Throw exception if product does not exist
            throw new NotFoundException($"Product with id '{id}' does not exist");
        }

        // Update entity
        product.Code = productUpdateDto.Code;
        product.Price = productUpdateDto.Price;
        product.IsAvailable = productUpdateDto.IsAvailable;
        product.Name = productUpdateDto.Name;
        product.Type = productUpdateDto.Type;

        try
        {
            // Update to db
            _productRepository.Update(product);
            await _unitOfWork.CommitAsync();
        }
        catch (UniqueConstraintException)
        {
            // Catch unique product code constraint exception, throw it to outer layer
            throw new ConflictException($"Product with code '{productUpdateDto.Code}' has already existed.");
        }

        return _mapper.Map<ProductDto>(product);
    }
}
