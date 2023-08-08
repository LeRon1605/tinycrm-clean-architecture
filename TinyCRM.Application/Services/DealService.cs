using TinyCRM.Application.Dtos.Deals;
using TinyCRM.Application.UnitOfWorks;
using TinyCRM.Domain.Exceptions.Deals;
using TinyCRM.Domain.Specifications.Deals;

namespace TinyCRM.Application.Services;

public class DealService : BaseService<Deal, int, DealDto, DealCreateDto, DealUpdateDto>, IDealService
{
    private readonly IDealRepository _dealRepository;
    private readonly IDealLineRepository _dealLineRepository;
    private readonly IProductRepository _productRepository;

    public DealService(
        IDealRepository dealRepository,
        IDealLineRepository dealLineRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(mapper, dealRepository, unitOfWork)
    {
        _dealRepository = dealRepository;
        _productRepository = productRepository;
        _dealLineRepository = dealLineRepository;
    }

    protected override Task<bool> IsValidOnDeleteAsync(Deal deal)
    {
        // Only allow delete deal on open status
        if (!DealSpecification.OpenSpecification.IsSatisfiedBy(deal))
        {
            throw new ProcessedDealException(deal.Id, deal.Status);
        }

        return Task.FromResult(true);
    }

    protected override Deal UpdateEntity(Deal deal, DealUpdateDto dealUpdateDto)
    {
        // Allow update description and source only for open and lost
        deal.Description = dealUpdateDto.Description;

        if (DealSpecification.OpenSpecification.IsSatisfiedBy(deal))
        {
            deal.Title = dealUpdateDto.Title;
            deal.EstimatedRevenue = dealUpdateDto.EstimatedRevenue;
            deal.Status = dealUpdateDto.Status;
        }

        return deal;
    }

    public async Task<DealStatisticDto> GetStatisticAsync()
    {
        return new DealStatisticDto()
        {
            OpenDeal = await _dealRepository.GetCountOpenDealAsync(),
            WonDeal = await _dealRepository.GetCountWonDealAsync(),
            AverageRevenue = await _dealRepository.GetAverageRevenueAsync(),
            TotalRevenue = await _dealRepository.GetTotalRevenueAsync(),
        };
    }

    public async Task<DealLineDto> AddLineAsync(int id, DealLineCreateDto dealLineCreateDto)
    {
        // 1. Check if deal is processed
        await CheckUnProcessedDealAsync(id);

        // 2. Check if product exist
        await CheckProductExistingAsync(dealLineCreateDto.ProductId);

        // 3. Add line
        var dealLine = _mapper.Map<DealLine>(dealLineCreateDto);
        dealLine.DealId = id;

        await _dealLineRepository.InsertAsync(dealLine);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<DealLineDto>(dealLine);
    }

    public async Task<DealLineDto> UpdateLineAsync(int id, int dealLineId, DealLineUpdateDto dealLineUpdateDto)
    {
        // 1. Check if deal is processed
        await CheckUnProcessedDealAsync(id);

        // 2. Check if dealLine exist
        var dealLine = await _dealLineRepository.FindByIdAsync(dealLineId);
        if (dealLine == null)
        {
            throw new ResourceNotFoundException(nameof(DealLine), dealLineId);
        }

        // 3. Check if line is belong to deal
        if (dealLine.DealId != id)
        {
            throw new LineNotBelongToDealException(dealLineId, id);
        }

        // 4. Check if product exist
        if (dealLine.ProductId != dealLineUpdateDto.ProductId)
        {
            await CheckProductExistingAsync(dealLineUpdateDto.ProductId);
        }

        // 5. Update line
        _mapper.Map(dealLineUpdateDto, dealLine);
        _dealLineRepository.Update(dealLine);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<DealLineDto>(dealLine);
    }

    public async Task RemoveLineAsync(int id, int dealLineId)
    {
        // 1. Check if deal is processed
        await CheckUnProcessedDealAsync(id);

        // 2. Check if dealLine exist
        var dealLine = await _dealLineRepository.FindByIdAsync(dealLineId);
        if (dealLine == null)
        {
            throw new ResourceNotFoundException(nameof(DealLine), dealLineId);
        }

        // 3. Check if line is belong to deal
        if (dealLine.DealId != id)
        {
            throw new LineNotBelongToDealException(dealLineId, id);
        }

        // 4. Remove line
        _dealLineRepository.Delete(dealLine);
        await _unitOfWork.CommitAsync();
    }

    public async Task<Deal> CheckUnProcessedDealAsync(int id)
    {
        var deal = await _dealRepository.FindByIdAsync(id);
        if (deal == null)
        {
            throw new ResourceNotFoundException(nameof(Deal), id);
        }

        if (DealSpecification.ProcessedSpecification.IsSatisfiedBy(deal))
        {
            throw new ProcessedDealException(id, deal.Status);
        }

        return deal;
    }

    public async Task<PagedResultDto<DealLineDto>> GetLinesAsync(int id, DealLineFilterAndPagingRequestDto filterParam)
    {
        await CheckExistingAsync(id);

        var getPagedLineForDealSpecification = filterParam.ToSpecification().And(new DealLineForDealSpecification(id));
        var data = await _dealLineRepository.GetPagedListAsync(getPagedLineForDealSpecification);
        var total = await _dealLineRepository.GetCountAsync(getPagedLineForDealSpecification);

        return new PagedResultDto<DealLineDto>()
        {
            Data = _mapper.Map<IEnumerable<DealLineDto>>(data),
            TotalPages = (int)Math.Ceiling(total * 1.0 / filterParam.Size)
        };
    }

    private async Task<bool> CheckProductExistingAsync(int productId)
    {
        var isProductExisting = await _productRepository.IsExistingAsync(productId);
        if (!isProductExisting)
        {
            throw new ResourceNotFoundException(nameof(Product), productId);
        }

        return true;
    }
}