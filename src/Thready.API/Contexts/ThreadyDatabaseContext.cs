namespace Thready.API.Contexts;

using Microsoft.EntityFrameworkCore;
using Thready.Models.Models;


public class ThreadyDatabaseContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public ThreadyDatabaseContext(IConfiguration configuration)
    {
        Configuration = configuration;
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
}