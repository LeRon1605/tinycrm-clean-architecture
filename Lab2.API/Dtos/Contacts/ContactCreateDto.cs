﻿namespace Lab2.API.Dtos;

public class ContactCreateDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int? AccountId { get; set; }
}