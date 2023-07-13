using Lab2.Domain.Repositories;

namespace Lab2.Domain;

public class DataContributor
{
    private readonly IAccountRepository _accountRepository;
    private readonly IContactRepository _contactRepository;
}
