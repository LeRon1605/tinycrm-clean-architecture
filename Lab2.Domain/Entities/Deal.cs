﻿using Lab2.Domain.Base;
using Lab2.Domain.Shared.Enums;

namespace Lab2.Domain.Entities;

public class Deal : Entity<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int EstimatedRevenue { get; set; }
    public DealStatus Status { get; set; }

    public int LeadId { get; set; }
    public Lead Lead { get; set; }

    public ICollection<DealLine> Lines { get; set; }
}
