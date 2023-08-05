using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Infrastructure.Persistent.Configurations;

public class DealLineEntityConfiguration : IEntityTypeConfiguration<DealLine>
{
    public void Configure(EntityTypeBuilder<DealLine> builder)
    {
        builder.ToTable(tb =>
        {
            tb.HasTrigger("UpdateTotalSale");
        });

        builder.HasOne(x => x.Product)
               .WithMany(x => x.Lines)
               .HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.Deal)
               .WithMany(x => x.Lines)
               .HasForeignKey(x => x.DealId);
    }
}