using Lab2.API.Dtos;
using Lab2.API.Dtos.Deals;
using Lab2.Domain.Entities;

namespace Lab2.API.Services;

public interface IDealService : IService<Deal, DealDto, DealCreateDto, DealUpdateDto>
{
    Task<DealStatisticDto> GetStatistic();
}
