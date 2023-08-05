using TinyCRM.Domain.Entities.Base;

namespace TinyCRM.Domain.Entities;

public class Contact : Entity<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public int? AccountId { get; set; }
    public Account? Account { get; set; }
}