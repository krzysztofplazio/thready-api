using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Core.Models;

namespace Thready.Infrastructure.Configurations.ContextConfigurations;

public class UsersEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.FirstName)
            .HasMaxLength(50)
            .IsRequired(true);
        
        builder.Property(e => e.FirstName)
            .HasMaxLength(50)
            .IsRequired(true);
        
        builder.Property(e => e.Avatar)
            .HasMaxLength(52428800)
            .IsRequired(false);

        builder.Property(e => e.Username)
            .HasMaxLength(10)
            .IsRequired(true);
        
        builder.Property(e => e.Password)
            .HasMaxLength(128)
            .IsRequired(true);

        builder.HasMany(u => u.CreatedThreads)
            .WithOne(tw => tw.CreatedBy)
            .HasForeignKey(tr => tr.CreatedByUserId);
        
        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        builder.Navigation(e => e.Role)
            .AutoInclude();
    }
}