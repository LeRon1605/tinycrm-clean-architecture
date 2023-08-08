using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TinyCRM.Infrastructure.Persistent.Configurations;

public class LeadEntityConfiguration : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.ToTable("Leads");

        builder.HasOne(x => x.Customer)
               .WithMany(x => x.Leads)
               .HasForeignKey(x => x.CustomerId);
    }
}