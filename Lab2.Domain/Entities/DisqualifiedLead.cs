﻿namespace Lab2.Domain.Entities;

public class DisqualifiedLead : Lead
{
    public string Reason { get; set; }
    public DateTime DisqualifiedDate { get; set; }
}