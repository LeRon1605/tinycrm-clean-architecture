﻿using Lab2.API.Dtos.Shared;
using Lab2.Domain.Shared.Enums;

namespace Lab2.API.Dtos;

public class DealFilterAndPagingRequestDto : PagingRequestDto
{
    public string Title { get; set; } = string.Empty;
    public DealStatus? Status { get; set; }
}
