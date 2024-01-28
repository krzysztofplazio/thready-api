using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Core.Models;

namespace Thready.Infrastructure.Configurations.ContextConfigurations;

public class ProjectPermissionsEntityTypeConfiguration : IEntityTypeConfiguration<ProjectPermission>
{
    public void Configure(EntityTypeBuilder<ProjectPermission> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn();
    }
}
