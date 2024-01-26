namespace Thready.API.Contexts;

using Microsoft.EntityFrameworkCore;
using Thready.API.Configurations.ContextConfigurations;
using Thready.Models.Models;


public class ThreadyDatabaseContext : DbContext
{
    public ThreadyDatabaseContext(DbContextOptions<ThreadyDatabaseContext> options) : base(options)
    {
    }

    public virtual DbSet<Attachment> Attachments { get; set; } = null!;
    public virtual DbSet<Comment> Comments { get; set; } = null!;
    public virtual DbSet<CustomField> CustomFields { get; set; } = null!;
    public virtual DbSet<CustomValue> CustomValues { get; set; } = null!;
    public virtual DbSet<Priority> Priorities { get; set; } = null!;
    public virtual DbSet<Project> Projects { get; set; } = null!;
    public virtual DbSet<ProjectPermission> ProjectPermissions { get; set; } = null!;
    public virtual DbSet<Role> Roles { get; set; } = null!;
    public virtual DbSet<State> States { get; set; } = null!;
    public virtual DbSet<ThreadHistory> ThreadHistories { get; set; } = null!;
    public virtual DbSet<ThreadWorkItem> Threads { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new AttachmentsEntityTypeConfiguration().Configure(modelBuilder.Entity<Attachment>());
        new CommentsEntityTypeConfiguration().Configure(modelBuilder.Entity<Comment>());
        new CustomFieldsEntityTypeConfiguration().Configure(modelBuilder.Entity<CustomField>());
        new CustomValuesEntityTypeConfiguration().Configure(modelBuilder.Entity<CustomValue>());
        new PrioritiesEntityTypeConfiguration().Configure(modelBuilder.Entity<Priority>());
        new ProjectsEntityTypeConfiguration().Configure(modelBuilder.Entity<Project>());
        new ProjectPermissionsEntityTypeConfiguration().Configure(modelBuilder.Entity<ProjectPermission>());
        new RolesEntityTypeConfiguration().Configure(modelBuilder.Entity<Role>());
        new StatesEntityTypeConfiguration().Configure(modelBuilder.Entity<State>());
        new ThreadHistoryEntityTypeConfiguration().Configure(modelBuilder.Entity<ThreadHistory>());
        new UsersEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
    }
}