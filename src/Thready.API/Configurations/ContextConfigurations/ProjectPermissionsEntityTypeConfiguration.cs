using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Models.Models;

namespace Thready.API.Configurations.ContextConfigurations;

public class ProjectPermissionsEntityTypeConfiguration : IEntityTypeConfiguration<ProjectPermission>
{
    public void Configure(EntityTypeBuilder<ProjectPermission> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
