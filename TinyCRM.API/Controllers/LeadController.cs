using TinyCRM.Application.Dtos.Deals;
using TinyCRM.Application.Dtos.Leads;

namespace TinyCRM.API.Controllers;

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
    [Authorize(Policy = Permissions.Leads.View)]
    [ProducesResponseType(typeof(PagedResultDto<LeadDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllLeadsAsync([FromQuery] LeadFilterAndPagingRequestDto leadFilterAndPagingRequestDto)
    {
        var leads = await _leadService.GetPagedAsync(leadFilterAndPagingRequestDto);
        return Ok(leads);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetDetailAsync))]
    [Authorize(Policy = Permissions.Leads.View)]
    [ProducesResponseType(typeof(LeadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(int id)
    {
        var lead = await _leadService.GetAsync(id);
        return Ok(lead);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Leads.Create)]
    [ProducesResponseType(typeof(LeadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateLeadAsync(LeadCreateDto leadCreateDto)
    {
        var lead = await _leadService.CreateAsync(leadCreateDto);
        return CreatedAtAction(nameof(GetDetailAsync), new { id = lead.Id }, lead);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.Leads.Edit)]
    [ProducesResponseType(typeof(LeadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLeadAsync(int id, LeadUpdateDto leadUpdateDto)
    {
        var lead = await _leadService.UpdateAsync(id, leadUpdateDto);
        return Ok(lead);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [Authorize(Policy = Permissions.Leads.Delete)]
    [ProducesResponseType(typeof(LeadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteLeadAsync(int id)
    {
        await _leadService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("account/{accountId}")]
    [Authorize(Policy = Permissions.Leads.View)]
    [ProducesResponseType(typeof(IEnumerable<LeadDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLeadsOfAccountAsync(int accountId, [FromQuery] LeadFilterAndPagingRequestDto filterParam)
    {
        var leads = await _leadService.GetByAccountAsync(accountId, filterParam);
        return Ok(leads);
    }

    [HttpPost("{id}/qualify")]
    [Authorize(Policy = Permissions.Leads.Qualify)]
    [ProducesResponseType(typeof(DealDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> QualifyLead(int id)
    {
        var deal = await _leadService.QualifyAsync(id);
        return CreatedAtAction(nameof(DealController.GetDetailAsync), "Deal", new { id = deal.Id }, deal);
    }

    [HttpPost("{id}/disqualify")]
    [Authorize(Policy = Permissions.Leads.Disqualify)]
    [ProducesResponseType(typeof(LeadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DisqualifyLeadAsync(int id, DisqualifiedLeadCreateDto disqualifiedLeadCreateDto)
    {
        var lead = await _leadService.DisqualifyAsync(id, disqualifiedLeadCreateDto);
        return Ok(lead);
    }

    [HttpGet("statistic")]
    [Authorize(Policy = Permissions.Deals.Statistic)]
    [ProducesResponseType(typeof(LeadStatisticDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLeadStatisticAsync()
    {
        var leadStatistic = await _leadService.GetStatisticAsync();
        return Ok(leadStatistic);
    }
}