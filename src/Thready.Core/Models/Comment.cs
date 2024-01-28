namespace Thready.Core.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int ThreadId { get; set; }
    public ThreadWorkItem Thread { get; set; } = null!;
    public DateTime AdditionDate { get; set; }
    public int CreatorId { get; set; }
    public User Creator { get; set; } = null!;
}