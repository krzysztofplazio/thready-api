using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Models.Models;

namespace Thready.API.Configurations.ContextConfigurations;

public class ProjectsEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Title)
            .HasMaxLength(50)
            .IsRequired(true);
        
        builder.Property(e => e.Description)
            .HasMaxLength(5000)
            .IsRequired(true);
        
        builder.Property(e => e.DueDate)
            .IsRequired(true);
        
        builder.HasOne(c => c.Creator)
            .WithMany(t => t.Projects)
            .HasForeignKey(f => f.CreatorId);
    }

}