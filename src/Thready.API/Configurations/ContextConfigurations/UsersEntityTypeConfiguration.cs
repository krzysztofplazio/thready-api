using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Models.Models;

namespace Thready.API.Configurations.ContextConfigurations;

public class UsersEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

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
            .HasMaxLength(64)
            .IsRequired(true);
        
        builder.HasOne(u => u.Role)
            .WithOne(r => r.User)
            .HasForeignKey<User>(u => u.RoleId);
    }
}