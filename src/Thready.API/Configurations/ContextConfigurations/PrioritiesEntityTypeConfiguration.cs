using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Models.Models;

namespace Thready.API.Configurations.ContextConfigurations;

public class PrioritiesEntityTypeConfiguration : IEntityTypeConfiguration<Priority>
{
    public void Configure(EntityTypeBuilder<Priority> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(30)
            .IsRequired(true);
    }
}