using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Core.Models;

namespace Thready.Infrastructure.Configurations.ContextConfigurations;

public class PrioritiesEntityTypeConfiguration : IEntityTypeConfiguration<Priority>
{
    public void Configure(EntityTypeBuilder<Priority> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Name)
            .HasMaxLength(30)
            .IsRequired(true);
    }
}