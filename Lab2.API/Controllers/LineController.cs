using Lab2.API.Dtos;
using Lab2.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

public class LineController : ApiController
{
    private readonly ILineService _lineService;

    public LineController(ILineService lineService)
    {
        _lineService = lineService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(DealLineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateLine(DealLineCreateDto dealLineCreateDto)
    {
        var lineDto = await _lineService.CreateAsync(dealLineCreateDto);
        return CreatedAtAction(nameof(GetDetail), new { id = lineDto.Id }, lineDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DealLineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetail(int id)
    {
        var lineDto = await _lineService.GetAsync(id);
        return Ok(lineDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DealLineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLine(int id, DealLineUpdateDto dealLineUpdateDto)
    {
        var lineDto = await _lineService.UpdateAsync(id, dealLineUpdateDto);
        return Ok(lineDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(DealLineDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLine(int id)
    {
        await _lineService.DeleteAsync(id);
        return Ok();
    }
}