using Lab2.API.Authorization;
using Lab2.API.Dtos;
using Lab2.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

[ApiController]
[Authorize]
[Route("api/lines")]
public class LineController : ControllerBase
{
    private readonly ILineService _lineService;
    private readonly IDealService _dealService;

    public LineController(ILineService lineService, IDealService dealService)
    {
        _lineService = lineService;
        _dealService = dealService;
    }

    [HttpPost]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(DealLineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateLineAsync(DealLineCreateDto dealLineCreateDto)
    {
        var lineDto = await _lineService.CreateAsync(dealLineCreateDto);
        return CreatedAtAction(nameof(GetDetailAsync), new { id = lineDto.Id }, lineDto);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetDetailAsync))]
    [ProducesResponseType(typeof(DealLineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(int id)
    {
        var lineDto = await _lineService.GetAsync(id);
        return Ok(lineDto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(DealLineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLineAsync(int id, DealLineUpdateDto dealLineUpdateDto)
    {
        var lineDto = await _lineService.UpdateAsync(id, dealLineUpdateDto);
        return Ok(lineDto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(DealLineDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLineAsync(int id)
    {
        await _lineService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("deal/{dealId}")]
    [ProducesResponseType(typeof(PagedResultDto<DealLineDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsInDealAsync(int dealId, [FromQuery] DealLineFilterAndPagingRequestDto dealLineFilterAndPagingRequestDto)
    {
        var lineDtos = await _dealService.GetProductsAsync(dealId, dealLineFilterAndPagingRequestDto);
        return Ok(lineDtos);
    }
}