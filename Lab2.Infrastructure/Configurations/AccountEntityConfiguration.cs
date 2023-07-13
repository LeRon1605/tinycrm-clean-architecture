using Lab2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab2.Infrastructure.Configurations;

public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(x => x.Id);

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
