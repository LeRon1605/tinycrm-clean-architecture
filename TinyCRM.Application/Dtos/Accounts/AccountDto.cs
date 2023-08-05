namespace TinyCRM.Application.Dtos.Accounts;

public class AccountDto : BasicAccountDto
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int TotalSales { get; set; }
}