﻿using Lab2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab2.Infrastructure.Configurations;

public class DealEntityConfiguration : IEntityTypeConfiguration<Deal>
{
    public void Configure(EntityTypeBuilder<Deal> builder)
    {
        builder.ToTable("Deals");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
               .HasMaxLength(256)
               .IsRequired();

        builder.HasOne(x => x.Customer)
               .WithMany(x => x.Deals)
               .HasForeignKey(x => x.CustomerId);

        builder.HasOne(x => x.Lead)
               .WithOne(x => x.Deal)
               .HasForeignKey("Deal", "LeadId");
    }
}