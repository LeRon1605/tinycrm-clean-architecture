using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Dtos.Deals;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Domain.Shared.Enums;

namespace Lab2.API.Services;

public class DealService : BaseService<Deal, DealDto, DealCreateDto, DealUpdateDto>, IDealService
{
    private readonly IDealRepository _dealRepository;

    public DealService(
        IDealRepository dealRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(mapper, dealRepository, unitOfWork)
    {
        _dealRepository = dealRepository;
    }

    protected override Task<bool> IsValidOnDeleteAsync(Deal deal)
    {
        // Only allow delete deal on open status
        if (deal.Status != DealStatus.Open)
        {
            throw new BadRequestException($"Can not delete deal which is on won or lost status!");
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

    public async Task<DealStatisticDto> GetStatistic()
    {
        return new DealStatisticDto()
        {
            OpenDeal = await _dealRepository.GetCountAsync(x => x.Status == DealStatus.Open),
            WonDeal = await _dealRepository.GetCountAsync(x => x.Status == DealStatus.Won),
            AverageRevenue = await _dealRepository.GetAverageRevenueAsync(),
            TotalRevenue = await _dealRepository.GetTotalRevenueAsync(),
        };
    }
}