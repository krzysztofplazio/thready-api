using System.Security.Cryptography;

namespace Thready.Models.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public int CreatorId { get; set; }
    public User Creator { get; set; } = null!;
    public IEnumerable<ThreadWorkItem> Threads { get; set; } = Enumerable.Empty<ThreadWorkItem>();
    public IEnumerable<ProjectPermission> ProjectPermissions { get; set; } = Enumerable.Empty<ProjectPermission>();
}