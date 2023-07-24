﻿using Lab2.Domain.Enums;

namespace Lab2.API.Dtos;

public class ProductCreateDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public ProductType Type { get; set; }
    public int Price { get; set; }
    public bool IsAvailable { get; set; }
}