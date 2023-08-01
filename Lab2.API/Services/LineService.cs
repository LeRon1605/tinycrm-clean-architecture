using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Domain.Specifications.Deals;

namespace Lab2.API.Services;

public class LineService : BaseService<DealLine, int, DealLineDto, DealLineCreateDto, DealLineUpdateDto>, ILineService
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

        _includePropsOnGet = nameof(DealLine.Product);
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

    private async Task<bool> CheckDealExistingAsync(int dealId)
    {
        var isDealExisting = await _dealRepository.IsExistingAsync(dealId);
        if (!isDealExisting)
        {
            throw new EntityNotFoundException(nameof(Deal), dealId);
        }

        return true;
    }

    private async Task<bool> CheckProductExistingAsync(int productId)
    {
        var isProductExisting = await _productRepository.IsExistingAsync(productId);
        if (!isProductExisting)
        {
            throw new EntityNotFoundException(nameof(Product), productId);
        }

        return true;
    }

    public async Task<PagedResultDto<DealLineDto>> GetByDealAsync(int dealId, DealLineFilterAndPagingRequestDto filterParam)
    {
        // 1. Check deal existing
        await CheckDealExistingAsync(dealId);

        // 2. Get deal line
        var getPagedDealLineForDealSpecification = filterParam.ToSpecification().And(new GetDealLineForDealSpecification(dealId));
        return await GetPagedAsync(getPagedDealLineForDealSpecification);
    }
}