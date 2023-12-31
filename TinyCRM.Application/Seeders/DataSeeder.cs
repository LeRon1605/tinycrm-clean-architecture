﻿using Microsoft.Extensions.Logging;
using TinyCRM.Application.Seeders.Interfaces;
using TinyCRM.Application.UnitOfWorks;
using TinyCRM.Domain.Common.Enums;

namespace TinyCRM.Application.Seeders;

public class DataSeeder : IDataSeeder
{
    private readonly IContactRepository _contactRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILeadRepository _leadRepository;
    private readonly IDealRepository _dealRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    private static IEnumerable<Account> _accounts;
    private static IEnumerable<Contact> _contacts;
    private static IEnumerable<Product> _products;
    private static IEnumerable<Lead> _leads;
    private static IEnumerable<Deal> _deals;

    public DataSeeder(
        IContactRepository contactRepository,
        IAccountRepository accountRepository,
        IProductRepository productRepository,
        ILeadRepository leadRepository,
        IDealRepository dealRepository,
        IUnitOfWork unitOfWork,
        ILogger<DataSeeder> logger)
    {
        _contactRepository = contactRepository;
        _accountRepository = accountRepository;
        _productRepository = productRepository;
        _leadRepository = leadRepository;
        _dealRepository = dealRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            if (
                !await _leadRepository.AnyAsync() &&
                !await _dealRepository.AnyAsync() &&
                !await _accountRepository.AnyAsync() &&
                !await _contactRepository.AnyAsync() &&
                !await _productRepository.AnyAsync()
            )
            {
                _logger.LogInformation("Begin seeding data...");

                _accounts = await SeedAccountAsync();
                _contacts = await SeedContactAsync();
                _products = await SeedProductAsync();
                _leads = await SeedLeadAsync();
                _deals = await SeedDealAsync();

                await _unitOfWork.CommitAsync();

                _logger.LogInformation("Seed data successfully!");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Seeding data failed!", ex);
        }
    }

    private async Task<IEnumerable<Account>> SeedAccountAsync()
    {
        var accounts = new List<Account>();

        for (var i = 1; i <= 10; i++)
        {
            accounts.Add(new Account()
            {
                Name = $"Account {i}",
                Email = $"account{i}@gmail.com",
                Address = $"Address - {i}",
                Phone = i.ToString(),
                TotalSales = 0
            });
        }

        await _accountRepository.InsertRangeAsync(accounts);
        return accounts;
    }

    private async Task<IEnumerable<Contact>> SeedContactAsync()
    {
        var contacts = new List<Contact>();
        var random = new Random();

        for (var i = 1; i <= 10; i++)
        {
            contacts.Add(new Contact()
            {
                Name = $"Contact {i}",
                Email = $"contact{i}@gmail.com",
                Phone = i.ToString(),
                Account = _accounts.ElementAt(random.Next(0, 9))
            });
        }

        await _contactRepository.InsertRangeAsync(contacts);
        return contacts;
    }

    private async Task<IEnumerable<Product>> SeedProductAsync()
    {
        var products = new List<Product>();
        var random = new Random();

        for (var i = 1; i <= 10; i++)
        {
            products.Add(new Product()
            {
                Code = $"P-{i}",
                Name = $"Product {i}",
                Price = random.Next(2000),
                IsAvailable = random.Next(2) == 1,
                Type = random.Next(2) == 1 ? ProductType.Service : ProductType.Physical
            });
        }

        await _productRepository.InsertRangeAsync(products);
        return products;
    }

    private async Task<IEnumerable<Lead>> SeedLeadAsync()
    {
        var leads = new List<Lead>();
        var random = new Random();

        for (var i = 1; i <= 10; i++)
        {
            switch (i % 3)
            {
                case 0:
                    leads.Add(new Lead()
                    {
                        Title = $"Lead {i}",
                        Description = $"Lead {i}",
                        Source = "Email",
                        Status = LeadStatus.Open,
                        Customer = _accounts.ElementAt(random.Next(0, 9)),
                        EstimatedRevenue = random.Next(5000)
                    });
                    break;

                case 1:
                    leads.Add(new Lead()
                    {
                        Title = $"Lead {i}",
                        Description = $"Lead {i}",
                        Source = "Email",
                        Status = LeadStatus.Qualified,
                        Customer = _accounts.ElementAt(random.Next(0, 9)),
                        EstimatedRevenue = random.Next(5000),
                        EndedDate = DateTime.Now
                    });
                    break;

                case 2:
                    leads.Add(new Lead()
                    {
                        Title = $"Lead {i}",
                        Description = $"Lead {i}",
                        Source = "Email",
                        Status = LeadStatus.Disqualified,
                        Customer = _accounts.ElementAt(random.Next(0, 9)),
                        EstimatedRevenue = random.Next(5000),
                        EndedDate = DateTime.Now,
                        Reason = "Reason",
                        ReasonDescription = "Reason description"
                    });
                    break;
            }
        }

        await _leadRepository.InsertRangeAsync(leads);
        return leads;
    }

    private async Task<IEnumerable<Deal>> SeedDealAsync()
    {
        var deals = new List<Deal>();
        var noneOpenLead = _leads.Where(x => x.Status != LeadStatus.Open);
        var random = new Random();

        for (var i = 1; i <= 10; i++)
        {
            var dealLines = new List<DealLine>();

            for (var j = 1; j <= 5; j++)
            {
                dealLines.Add(new DealLine()
                {
                    PricePerUnit = random.Next(1, 10),
                    Product = _products.ElementAt(random.Next(0, 9)),
                    Quantity = random.Next(1, 10),
                });
            }
            deals.Add(new Deal()
            {
                Title = $"Deal {i}",
                Description = $"Deal {i}",
                Status = random.Next(2) == 1 ? DealStatus.Open : DealStatus.Won,
                Lead = noneOpenLead.ElementAt(random.Next(0, noneOpenLead.Count() - 1)),
                EstimatedRevenue = random.Next(5000),
                Lines = dealLines
            });
        }

        await _dealRepository.InsertRangeAsync(deals);
        return deals;
    }
}