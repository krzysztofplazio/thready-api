using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Core.Models;

namespace Thready.Infrastructure.Configurations.ContextConfigurations;

public class ThreadHistoryEntityTypeConfiguration : IEntityTypeConfiguration<ThreadHistory>
{
    public void Configure(EntityTypeBuilder<ThreadHistory> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Type)
            .HasMaxLength(10)
            .IsRequired(true);

        builder.Property(e => e.Description)
            .HasMaxLength(100)
            .IsRequired(true);

        builder.HasOne(th => th.User)
            .WithMany(u => u.ThreadHistories)
            .HasForeignKey(th => th.UserId);
    }
}