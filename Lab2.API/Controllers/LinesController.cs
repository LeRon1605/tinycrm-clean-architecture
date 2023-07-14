using Lab2.API.Dtos;
using Lab2.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LinesController : ApiController
{
    private readonly ILineService _lineService;
    public LinesController(ILineService lineService)
    {
        _lineService = lineService;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLine(int id, DealLineUpdateDto dealLineUpdateDto)
    {
        DealLineDto line = await _lineService.UpdateAsync(id, dealLineUpdateDto);
        return Ok(line);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLine(int id)
    {
        await _lineService.DeleteAsync(id);
        return Ok();
    }
}
