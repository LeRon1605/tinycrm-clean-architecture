using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TinyCRM.Infrastructure.Persistent.Configurations;

public class PermissionEntityConfiguration : IEntityTypeConfiguration<PermissionContent>
{
    public void Configure(EntityTypeBuilder<PermissionContent> builder)
    {
        builder.ToTable("Permissions");

        builder.HasIndex(x => x.Name)
               .IsUnique();
    }
}