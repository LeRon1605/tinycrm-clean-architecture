using Lab2.API.Dtos;
using Lab2.API.Filters;
using Lab2.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

[ApiController]
[Route("api/leads")]
public class LeadController : ControllerBase
{
    private readonly ILeadService _leadService;

    public LeadController(ILeadService leadService)
    {
        _leadService = leadService;
    }

    [HttpGet]
    [SortQueryConstraint(Fields = "Title, Customer")]
    [ProducesResponseType(typeof(PagedResultDto<LeadDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllLeadsAsync([FromQuery] LeadFilterAndPagingRequestDto leadFilterAndPagingRequestDto)
    {
        var leadDtos = await _leadService.GetPagedAsync(leadFilterAndPagingRequestDto);
        return Ok(leadDtos);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetDetailAsync))]
    [ProducesResponseType(typeof(LeadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(int id)
    {
        var leadDto = await _leadService.GetAsync(id);
        return Ok(leadDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(LeadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateLeadAsync(LeadCreateDto leadCreateDto)
    {
        var leadDto = await _leadService.CreateAsync(leadCreateDto);
        return CreatedAtAction(nameof(GetDetailAsync), new { id = leadDto.Id }, leadDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(LeadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLeadAsync(int id, LeadUpdateDto leadUpdateDto)
    {
        var leadDto = await _leadService.UpdateAsync(id, leadUpdateDto);
        return Ok(leadDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(LeadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteLeadAsync(int id)
    {
        await _leadService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/qualify")]
    [ProducesResponseType(typeof(DealDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> QualifyLead(int id)
    {
        var dealDto = await _leadService.QualifyAsync(id);
        return CreatedAtAction(nameof(DealController.GetDetailAsync), "Deal", new { id = dealDto.Id }, dealDto);
    }

    [HttpPost("{id}/disqualify")]
    [ProducesResponseType(typeof(LeadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DisqualifyLeadAsync(int id, DisqualifiedLeadCreateDto disqualifiedLeadCreateDto)
    {
        var leadDto = await _leadService.DisqualifyAsync(id, disqualifiedLeadCreateDto);
        return Ok(leadDto);
    }

    [HttpGet("statistic")]
    [ProducesResponseType(typeof(LeadStatisticDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLeadStatisticAsync()
    {
        var leadStatisticDto = await _leadService.GetStatisticAsync();
        return Ok(leadStatisticDto);
    }
}