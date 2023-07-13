namespace Lab2.Domain.Entities;

public class Account : IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int TotalSales { get; set; }

    public ICollection<Lead> Leads { get; set; }
    public ICollection<Deal> Deals { get; set; }
    public ICollection<Contact> Contacts { get; set; }
}