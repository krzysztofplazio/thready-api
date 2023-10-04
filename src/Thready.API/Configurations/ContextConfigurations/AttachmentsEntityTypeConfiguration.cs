using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Models.Models;

namespace Thready.API.Configurations.ContextConfigurations;

public class AttachmentsEntityTypeConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired(true);

        builder.Property(e => e.Content)
            .HasMaxLength(52_428_800)
            .IsRequired(true);
    }
}