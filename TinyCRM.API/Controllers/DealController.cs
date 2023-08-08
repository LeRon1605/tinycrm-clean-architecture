using TinyCRM.Application.Dtos.Deals;

namespace TinyCRM.API.Controllers;

[ApiController]
[Route("api/deals")]
public class DealController : ControllerBase
{
    private readonly IDealService _dealService;

    public DealController(IDealService dealService)
    {
        _dealService = dealService;
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Deals.View)]
    [ProducesResponseType(typeof(PagedResultDto<DealDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDealsAsync([FromQuery] DealFilterAndPagingRequestDto dealFilterAndPagingRequestDto)
    {
        var deals = await _dealService.GetPagedAsync(dealFilterAndPagingRequestDto);
        return Ok(deals);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.Deals.View)]
    [ActionName(nameof(GetDetailAsync))]
    [ProducesResponseType(typeof(DealDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(int id)
    {
        var deal = await _dealService.GetAsync(id);
        return Ok(deal);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.Deals.Edit)]
    [ProducesResponseType(typeof(DealDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDealAsync(int id, DealUpdateDto dealUpdateDto)
    {
        var deal = await _dealService.UpdateAsync(id, dealUpdateDto);
        return Ok(deal);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Deals.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDealAsync(int id)
    {
        await _dealService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("statistic")]
    [Authorize(Policy = Permissions.Deals.Statistic)]
    [ProducesResponseType(typeof(DealStatisticDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDealStatisticAsync()
    {
        var dealStatistic = await _dealService.GetStatisticAsync();
        return Ok(dealStatistic);
    }

    [HttpGet("{id}/lines")]
    [Authorize(Policy = Permissions.Deals.ViewProduct)]
    [ProducesResponseType(typeof(PagedResultDto<DealLineDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLinesAsync(int id, [FromQuery] DealLineFilterAndPagingRequestDto dealLineFilterAndPagingRequestDto)
    {
        var dealLines = await _dealService.GetLinesAsync(id, dealLineFilterAndPagingRequestDto);
        return Ok(dealLines);
    }

    [HttpPost("{id}/lines")]
    [Authorize(Policy = Permissions.Deals.CreateProduct)]
    [ProducesResponseType(typeof(DealLineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateLineAsync(int id, DealLineCreateDto dealLineCreateDto)
    {
        var dealLine = await _dealService.AddLineAsync(id, dealLineCreateDto);
        return Ok(dealLine);
    }

    [HttpDelete("{id}/lines/{dealLineId}")]
    [Authorize(Policy = Permissions.Deals.DeleteProduct)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveLineAsync(int id, int dealLineId)
    {
        await _dealService.RemoveLineAsync(id, dealLineId);
        return NoContent();
    }

    [HttpPut("{id}/lines/{dealLineId}")]
    [Authorize(Policy = Permissions.Deals.EditProduct)]
    [ProducesResponseType(typeof(DealLineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLineAsync(int id, int dealLineId, DealLineUpdateDto dealLineUpdateDto)
    {
        var dealLine = await _dealService.UpdateLineAsync(id, dealLineId, dealLineUpdateDto);
        return Ok(dealLine);
    }
}