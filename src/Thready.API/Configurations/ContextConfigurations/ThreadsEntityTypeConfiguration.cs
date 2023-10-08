using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thready.Models.Models;

namespace Thready.API.Configurations.ContextConfigurations;

public class ThreadsEntityTypeConfiguration : IEntityTypeConfiguration<ThreadWorkItem>
{
    public void Configure(EntityTypeBuilder<ThreadWorkItem> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.Title)
            .HasMaxLength(50)
            .IsRequired(true);
        
        builder.HasOne(t => t.State)
            .WithOne(s => s.ThreadWorkItem)
            .HasForeignKey<ThreadWorkItem>(t => t.StateId);

         builder.HasOne(t => t.Priority)
            .WithOne(p => p.ThreadWorkItem)
            .HasForeignKey<ThreadWorkItem>(t => t.PriorityId);

        builder.Property(e => e.Description)
            .HasMaxLength(5000)
            .IsRequired(true);

        builder.HasMany(t => t.ThreadHistories)
            .WithOne(th => th.Thread)
            .HasForeignKey(th => th.ThreadId);
        
        builder.Property(e => e.CreateDate)
            .IsRequired(true);
        
        builder.Property(e => e.ModificateDate)
            .IsRequired(false);

        builder.HasOne(t => t.OwnedBy)
            .WithMany(u => u.OwnedThreads)
            .HasForeignKey(t => t.OwnedByUserId);
        
        builder.HasMany(t => t.Comments)
            .WithOne(c => c.Thread)
            .HasForeignKey(c => c.ThreadId);
        
        builder.HasMany(t => t.Attachments)
            .WithOne(a => a.Thread)
            .HasForeignKey(a => a.ThreadId);

        builder.HasOne(t => t.Project)
            .WithMany(p => p.Threads)
            .HasForeignKey(t => t.ProjectId);
        
        builder.HasMany(t => t.CustomValues)
            .WithOne(cv => cv.Thread)
            .HasForeignKey(cv => cv.ThreadId);
    }
}