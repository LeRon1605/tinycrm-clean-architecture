namespace Lab2.Domain.Entities;

public class Contact : IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public int? AccountId { get; set; }
    public Account? Account { get; set; }
}