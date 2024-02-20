using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Core.Enums;
using Thready.Core.Models;

namespace Thready.Infrastructure.Configurations.ContextConfigurations;

public class RolesEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Name)
            .HasMaxLength(30)
            .HasConversion(
               v => v.ToString(),
               v => (RoleEnum)Enum.Parse(typeof(RoleEnum), v, false))
            .IsRequired(true);

        builder.Property(e => e.Priority)
            .IsRequired(true);
    }
}