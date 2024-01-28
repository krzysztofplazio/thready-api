namespace Thready.Core.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Priority { get; set; }
    public User User { get; set; } = null!;
}