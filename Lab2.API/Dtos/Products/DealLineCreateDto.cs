﻿namespace Lab2.API.Dtos;

public class DealLineCreateDto
{
    public int PricePerUnit { get; set; }
    public int Quantity { get; set; }
    public string ProductId { get; set; }
}