﻿using Lab2.API.Authorization;
using Lab2.API.Dtos;
using Lab2.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

[ApiController]
[Authorize]
[Route("api/leads")]
public class LeadController : ControllerBase
{
    private readonly ILeadService _leadService;

    public LeadController(ILeadService leadService)
    {
        _leadService = leadService;
    }

    [HttpGet]
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
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(LeadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateLeadAsync(LeadCreateDto leadCreateDto)
    {
        var leadDto = await _leadService.CreateAsync(leadCreateDto);
        return CreatedAtAction(nameof(GetDetailAsync), new { id = leadDto.Id }, leadDto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(LeadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLeadAsync(int id, LeadUpdateDto leadUpdateDto)
    {
        var leadDto = await _leadService.UpdateAsync(id, leadUpdateDto);
        return Ok(leadDto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(LeadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteLeadAsync(int id)
    {
        await _leadService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/qualify")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(DealDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> QualifyLead(int id)
    {
        var dealDto = await _leadService.QualifyAsync(id);
        return CreatedAtAction(nameof(DealController.GetDetailAsync), "Deal", new { id = dealDto.Id }, dealDto);
    }

    [HttpPost("{id}/disqualify")]
    [Authorize(Roles = AppRole.Admin)]
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