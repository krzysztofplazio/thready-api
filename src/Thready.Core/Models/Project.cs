using System.Security.Cryptography;

namespace Thready.Core.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public int CreatorId { get; set; }
    public User Creator { get; set; } = null!;
    public ICollection<ThreadWorkItem> Threads { get; set; } = [];
    public ICollection<ProjectPermission> ProjectPermissions { get; set; } = [];
}