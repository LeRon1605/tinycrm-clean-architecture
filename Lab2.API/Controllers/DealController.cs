using Lab2.API.Authorization;
using Lab2.API.Dtos;
using Lab2.API.Dtos.Deals;
using Lab2.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

[ApiController]
[Authorize]
[Route("api/deals")]
public class DealController : ControllerBase
{
    private readonly IDealService _dealService;

    public DealController(IDealService dealService)
    {
        _dealService = dealService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResultDto<DealDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDealsAsync([FromQuery] DealFilterAndPagingRequestDto dealFilterAndPagingRequestDto)
    {
        var dealDtos = await _dealService.GetPagedAsync(dealFilterAndPagingRequestDto);
        return Ok(dealDtos);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetDetailAsync))]
    [ProducesResponseType(typeof(DealDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(int id)
    {
        var dealDto = await _dealService.GetAsync(id);
        return Ok(dealDto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(DealDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDealAsync(int id, DealUpdateDto dealUpdateDto)
    {
        var dealDto = await _dealService.UpdateAsync(id, dealUpdateDto);
        return Ok(dealDto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDealAsync(int id)
    {
        await _dealService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("statistic")]
    [ProducesResponseType(typeof(DealStatisticDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDealStatisticAsync()
    {
        var dealStatisticDto = await _dealService.GetStatisticAsync();
        return Ok(dealStatisticDto);
    }
}