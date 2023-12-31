﻿namespace TinyCRM.Application.Dtos.Contacts;

public class ContactDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int? AccountId { get; set; }
}