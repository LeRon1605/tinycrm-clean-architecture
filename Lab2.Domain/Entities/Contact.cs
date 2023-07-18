using Lab2.Domain.Base;

namespace Lab2.Domain.Entities;

public class Contact : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public int? AccountId { get; set; }
    public Account? Account { get; set; }
}