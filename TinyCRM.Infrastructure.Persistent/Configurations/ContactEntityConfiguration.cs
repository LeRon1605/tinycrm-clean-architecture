using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TinyCRM.Infrastructure.Persistent.Configurations;

public class ContactEntityConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");

        builder.Property(x => x.Name)
               .HasMaxLength(256)
               .IsRequired();

        builder.Property(x => x.Email)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.Phone)
               .HasMaxLength(30)
               .IsRequired(false);

        builder.HasOne(x => x.Account)
               .WithMany(x => x.Contacts)
               .HasForeignKey(x => x.AccountId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);
    }
}