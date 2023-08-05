using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Infrastructure.Persistent.Configurations;

public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.Property(x => x.Name)
               .HasMaxLength(256)
               .IsRequired();

        builder.Property(x => x.Email)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.Phone)
               .HasMaxLength(30)
               .IsRequired(false);

        builder.Property(x => x.Address)
               .HasMaxLength(100)
               .IsRequired();
    }
}