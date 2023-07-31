using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Services;

public interface ILineService : IService<DealLine, int, DealLineDto, DealLineCreateDto, DealLineUpdateDto>
{
}