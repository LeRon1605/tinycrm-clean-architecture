using Lab2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab2.Infrastructure.Configurations;

public class DealLineEntityConfiguration : IEntityTypeConfiguration<DealLine>
{
    public void Configure(EntityTypeBuilder<DealLine> builder)
    {
        builder.ToTable("DealLines");

        builder.HasOne(x => x.Product)
               .WithMany(x => x.Lines)
               .HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.Deal)
               .WithMany(x => x.Lines)
               .HasForeignKey(x => x.DealId);
    }
}