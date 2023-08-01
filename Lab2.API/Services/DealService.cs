using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Dtos.Deals;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Domain.Specifications;
using Lab2.Domain.Specifications.Deals;

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
        if (!DealSpecification.OpenSpecification.IsSatisfiedBy(deal))
        {
            throw new InvalidRemoveDealException(deal.Id);
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
}