using TinyCRM.Infrastructure.Identity;

namespace TinyCRM.Infrastructure.Persistent;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Deal> Deals { get; set; }
    public DbSet<DealLine> DealLines { get; set; }
    public DbSet<Lead> Leads { get; set; }
    public DbSet<PermissionContent> Permissions { get; set; }
    public DbSet<PermissionGrant> PermissionGrants { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}