namespace Thready.Models.Models;

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
    public IEnumerable<ProjectPermission> UsersProjectPermissions { get; set; } = null!;
    public IEnumerable<Project> Projects { get; set; } = Enumerable.Empty<Project>();
    public IEnumerable<ThreadWorkItem> CreatedThreads { get; set; } = Enumerable.Empty<ThreadWorkItem>();
    public IEnumerable<ThreadWorkItem> OwnedThreads { get; set; } = Enumerable.Empty<ThreadWorkItem>();
    public IEnumerable<ThreadHistory> ThreadHistories { get; set; } = Enumerable.Empty<ThreadHistory>();
}