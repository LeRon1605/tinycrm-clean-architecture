using Lab2.API.Dtos;
using Lab2.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

public class LeadsController : ApiController
{
    private readonly ILeadService _leadService;
    public LeadsController(ILeadService leadService)
    {
        _leadService = leadService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLeads([FromQuery] LeadFilterAndPagingRequestDto leadFilterAndPagingRequestDto)
    {
        PagedResultDto<LeadDto> leads = await _leadService.GetAllAsync(leadFilterAndPagingRequestDto);
        return Ok(leads);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail(int id)
    {
        LeadDto leadDto = await _leadService.GetAsync(id);
        return Ok(leadDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLead(LeadCreateDto leadCreateDto)
    {
        LeadDto leadDto = await _leadService.CreateAsync(leadCreateDto);
        return CreatedAtAction(nameof(GetDetail), new { id = leadDto.Id }, leadDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLead(int id, LeadUpdateDto leadUpdateDto)
    {
        LeadDto leadDto = await _leadService.UpdateAsync(id, leadUpdateDto);
        return Ok(leadDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLead(int id)
    {
        await _leadService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/qualify")]
    public async Task<IActionResult> QualifyLead(int id)
    {
        DealDto deal = await _leadService.QualifyAsync(id);
        return Ok(deal);
    }

    [HttpPost("{id}/disqualify")]
    public async Task<IActionResult> DisqualifyLead(int id, DisqualifiedLeadCreateDto disqualifiedLeadCreateDto)
    {
        LeadDto lead = await _leadService.DisqualifyAsync(id, disqualifiedLeadCreateDto);
        return Ok(lead);
    }
}
