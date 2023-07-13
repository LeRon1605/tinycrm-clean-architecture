using Lab2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab2.Infrastructure.Configurations;

public class DisqualifiedLeadEntityConfiguration : IEntityTypeConfiguration<DisqualifiedLead>
{
    public void Configure(EntityTypeBuilder<DisqualifiedLead> builder)
    {
        builder.ToTable("DisqualifiedLeads");
    }
}
