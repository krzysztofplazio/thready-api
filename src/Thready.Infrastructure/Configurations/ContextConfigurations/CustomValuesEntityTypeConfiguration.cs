using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Core.Models;

namespace Thready.Infrastructure.Configurations.ContextConfigurations;

public class CustomValuesEntityTypeConfiguration : IEntityTypeConfiguration<CustomValue>
{
    public void Configure(EntityTypeBuilder<CustomValue> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Value)
            .HasMaxLength(1000)
            .HasDefaultValue(string.Empty)
            .IsRequired(true);

        builder.Property(e => e.CreateDate)
            .IsRequired(true);
    }
}