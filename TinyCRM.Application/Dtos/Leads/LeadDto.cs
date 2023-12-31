﻿using TinyCRM.Application.Dtos.Accounts;
using TinyCRM.Domain.Common.Enums;

namespace TinyCRM.Application.Dtos.Leads;

public class LeadDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    public int EstimatedRevenue { get; set; }
    public LeadStatus Status { get; set; }
    public BasicAccountDto Customer { get; set; }
    public string? Reason { get; set; }
    public string? ReasonDescription { get; set; }
    public DateTime? EndedDate { get; set; }
}