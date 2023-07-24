﻿using Lab2.API.Dtos;
using Lab2.API.Dtos.Deals;
using Lab2.API.Filters;
using Lab2.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.API.Controllers;

[ApiController]
[Route("api/deals")]
public class DealController : ControllerBase
{
    private readonly IDealService _dealService;
    private readonly ILineService _lineService;

    public DealController(IDealService dealService, ILineService lineService)
    {
        _dealService = dealService;
        _lineService = lineService;
    }

    [HttpGet]
    [SortQueryConstraint(Fields = "Title")]
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
    [ProducesResponseType(typeof(DealDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDealAsync(int id, DealUpdateDto dealUpdateDto)
    {
        var dealDto = await _dealService.UpdateAsync(id, dealUpdateDto);
        return Ok(dealDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDealAsync(int id)
    {
        await _dealService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/lines")]
    [SortQueryConstraint(Fields = "Code, Name, TotalAmount")]
    [ProducesResponseType(typeof(PagedResultDto<DealLineDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsInDealAsync(int id, [FromQuery] DealLineFilterAndPagingRequestDto dealLineFilterAndPagingRequestDto)
    {
        var lineDtos = await _lineService.GetProductsInDealAsync(id, dealLineFilterAndPagingRequestDto);
        return Ok(lineDtos);
    }

    [HttpGet("statistic")]
    [ProducesResponseType(typeof(DealStatisticDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDealStatisticAsync()
    {
        var dealStatisticDto = await _dealService.GetStatisticAsync();
        return Ok(dealStatisticDto);
    }
}