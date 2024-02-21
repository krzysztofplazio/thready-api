namespace Thready.Core.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public byte[]? Avatar { get; set; } = null!;
    public string Username { get; set; } = null!;
    public byte[] Password { get; set; } = null!;
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public ICollection<ProjectPermission> UsersProjectPermissions { get; set; } = null!;
    public ICollection<Project> Projects { get; set; } = [];
    public ICollection<ThreadWorkItem> CreatedThreads { get; set; } = [];
    public ICollection<ThreadWorkItem> OwnedThreads { get; set; } = [];
    public ICollection<ThreadHistory> ThreadHistories { get; set; } = [];
    public ICollection<Attachment> UplodedAttachments { get; set; } = [];
}