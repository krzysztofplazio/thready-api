using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Core.Models;

namespace Thready.Infrastructure.Configurations.ContextConfigurations;

public class AttachmentsEntityTypeConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired(true);

        builder.Property(e => e.Content)
            .HasMaxLength(52_428_800)
            .IsRequired(true);

        builder.HasOne(a => a.User)
            .WithMany(u => u.UplodedAttachments)
            .HasForeignKey(a => a.UserId);
    }
}