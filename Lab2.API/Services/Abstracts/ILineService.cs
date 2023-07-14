using Lab2.API.Dtos;

namespace Lab2.API.Services;

public interface ILineService
{
    Task DeleteAsync(int id);
    Task<DealLineDto> UpdateAsync(int id, DealLineUpdateDto dealLineUpdateDto);
}
