using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Models.Models;

namespace Thready.API.Configurations.ContextConfigurations;
public class CustomFieldsEntityTypeConfiguration : IEntityTypeConfiguration<CustomField>
{
    public void Configure(EntityTypeBuilder<CustomField> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(40)
            .IsRequired(true);
        
        builder.Property(e => e.Type)
            .HasMaxLength(40)
            .IsRequired(true);

        builder.Property(e => e.IsRequired)
            .IsRequired(true);
        
        builder.HasMany(cf => cf.CustomValues)
            .WithOne(cv => cv.CustomField)
            .HasForeignKey(cv => cv.FieldId);
    }
}