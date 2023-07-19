using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.API.Extensions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

public class LineService : BaseService<DealLine, DealLineDto, DealLineCreateDto, DealLineUpdateDto>, ILineService
{
    private readonly IDealRepository _dealRepository;
    private readonly IProductRepository _productRepository;

    public LineService(
        IDealRepository dealRepository,
        IDealLineRepository dealLineRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(mapper, dealLineRepository, unitOfWork)
    {
        _dealRepository = dealRepository;
        _productRepository = productRepository;

        IncludePropsOnGet = nameof(DealLine.Product);
    }

    protected override async Task<bool> IsValidOnInsertAsync(DealLineCreateDto entityCreateDto)
    {
        // Check if deal exist
        await CheckDealExistingAsync(entityCreateDto.DealId);

        // Check if product exist
        await CheckProductExistingAsync(entityCreateDto.ProductId);

        return true;
    }

    protected override async Task<bool> IsValidOnUpdateAsync(DealLine line, DealLineUpdateDto dealLineUpdateDto)
    {
        if (dealLineUpdateDto.ProductId != line.ProductId)
        {
            // Check if product is exist
            await CheckProductExistingAsync(dealLineUpdateDto.ProductId);
        }

        return true;
    }

    protected override DealLine UpdateEntity(DealLine line, DealLineUpdateDto dealLineUpdateDto)
    {
        line.ProductId = dealLineUpdateDto.ProductId;
        line.PricePerUnit = dealLineUpdateDto.PricePerUnit;
        line.Quantity = dealLineUpdateDto.Quantity;

        return line;
    }

    public async Task<PagedResultDto<DealLineDto>> GetProductsInDealAsync(int dealId, DealLineFilterAndPagingRequestDto filterParam)
    {
        // Check if deal exist
        await CheckDealExistingAsync(dealId);

        return await GetPagedAsync(skip: (filterParam.Page - 1) * filterParam.Size,
                                   take: filterParam.Size,
                                   expression: filterParam.ToExpression().JoinWith(x => x.DealId == dealId),
                                   sorting: filterParam.BuildSortingParam());
    }

    private async Task<bool> CheckDealExistingAsync(int dealId)
    {
        var isDealExisting = await _dealRepository.AnyAsync(x => x.Id == dealId);
        if (!isDealExisting)
        {
            throw new EntityNotFoundException("Deal", dealId);
        }

        return true;
    }

    private async Task<bool> CheckProductExistingAsync(int productId)
    {
        var isProductExisting = await _productRepository.AnyAsync(x => x.Id == productId);
        if (!isProductExisting)
        {
            throw new EntityNotFoundException("Product", productId);
        }

        return true;
    }
}