using Lab2.Domain.Entities;
using Lab2.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Deal> Deals { get; set; }
    public DbSet<DealLine> DealLines { get; set; }
    public DbSet<Lead> Lead { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        new AccountEntityConfiguration().Configure(modelBuilder.Entity<Account>());
        new ContactEntityConfiguration().Configure(modelBuilder.Entity<Contact>());
        new ProductEntityConfiguration().Configure(modelBuilder.Entity<Product>());
        new LeadEntityConfiguration().Configure(modelBuilder.Entity<Lead>());
        new DealEntityConfiguration().Configure(modelBuilder.Entity<Deal>());
        new DealLineEntityConfiguration().Configure(modelBuilder.Entity<DealLine>());
    }
}