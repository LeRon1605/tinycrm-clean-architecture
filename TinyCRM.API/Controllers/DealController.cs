using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Application.Dtos.Deals;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Application.Services.Abstracts;
using TinyCRM.Domain.Common.Constants;

namespace TinyCRM.API.Controllers;

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

    [HttpGet("{id}/lines")]
    [ProducesResponseType(typeof(PagedResultDto<DealLineDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLinesAsync(int id, [FromQuery] DealLineFilterAndPagingRequestDto dealLineFilterAndPagingRequestDto)
    {
        var dealLineDtos = await _dealService.GetLinesAsync(id, dealLineFilterAndPagingRequestDto);
        return Ok(dealLineDtos);
    }

    [HttpPost("{id}/lines")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(DealLineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateLineAsync(int id, DealLineCreateDto dealLineCreateDto)
    {
        var dealLineDto = await _dealService.AddLineAsync(id, dealLineCreateDto);
        return Ok(dealLineDto);
    }

    [HttpDelete("{id}/lines/{dealLineId}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveLineAsync(int id, int dealLineId)
    {
        await _dealService.RemoveLineAsync(id, dealLineId);
        return NoContent();
    }

    [HttpPut("{id}/lines/{dealLineId}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(DealLineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLineAsync(int id, int dealLineId, DealLineUpdateDto dealLineUpdateDto)
    {
        var dealLineDto = await _dealService.UpdateLineAsync(id, dealLineId, dealLineUpdateDto);
        return Ok(dealLineDto);
    }
}