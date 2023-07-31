using Lab2.Domain.Base;

namespace Lab2.Domain.Entities;

public class Account : Entity<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int TotalSales { get; set; }

    public ICollection<Lead> Leads { get; set; }
    public ICollection<Contact> Contacts { get; set; }
}