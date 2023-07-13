using Lab2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab2.Infrastructure.Configurations;

public class QualifiedLeadEntityConfiguration : IEntityTypeConfiguration<QualifiedLead>
{
    public void Configure(EntityTypeBuilder<QualifiedLead> builder)
    {
        builder.ToTable("QualifiedLeads");
    }
}
