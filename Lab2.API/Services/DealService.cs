using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Dtos.Deals;
using Lab2.API.Exceptions;
using Lab2.API.Extensions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Enums;
using Lab2.Domain.Repositories;

namespace Lab2.API.Services;

public class DealService : BaseService<Deal, int, DealDto, DealCreateDto, DealUpdateDto>, IDealService
{
    private readonly IDealRepository _dealRepository;
    private readonly IDealLineRepository _dealLineRepository;

    public DealService(
        IDealRepository dealRepository,
        IDealLineRepository dealLineRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(mapper, dealRepository, unitOfWork)
    {
        _dealRepository = dealRepository;
        _dealLineRepository = dealLineRepository;
    }

    protected override Task<bool> IsValidOnDeleteAsync(Deal deal)
    {
        // Only allow delete deal on open status
        if (deal.Status != DealStatus.Open)
        {
            throw new InvalidRemoveDealException(deal.Id);
        }

        return Task.FromResult(true);
    }

    protected override Deal UpdateEntity(Deal deal, DealUpdateDto dealUpdateDto)
    {
        // Allow update description and source only for open and lost
        deal.Description = dealUpdateDto.Description;

        if (deal.Status == DealStatus.Open)
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
            OpenDeal = await _dealRepository.GetCountAsync(x => x.Status == DealStatus.Open),
            WonDeal = await _dealRepository.GetCountAsync(x => x.Status == DealStatus.Won),
            AverageRevenue = await _dealRepository.GetAverageRevenueAsync(),
            TotalRevenue = await _dealRepository.GetTotalRevenueAsync(),
        };
    }

    public async Task<PagedResultDto<DealLineDto>> GetProductsAsync(int id, DealLineFilterAndPagingRequestDto filterParam)
    {
        // 1. Check account existing
        await CheckExistingAsync(id);

        // 2. Get contacts 
        var pagedResult = await _dealLineRepository.GetPagedResultAsync(
                                                    skip: (filterParam.Page - 1) * filterParam.Size,
                                                    take: filterParam.Size,
                                                    filterParam.ToExpression().JoinWith(x => x.DealId == id),
                                                    filterParam.BuildSortingParam(),
                                                    tracking: false,
                                                    includeProps: nameof(DealLine.Product));

        return _mapper.Map<PagedResultDto<DealLineDto>>(pagedResult);
    }
}