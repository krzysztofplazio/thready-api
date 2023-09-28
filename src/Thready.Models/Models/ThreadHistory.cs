namespace Thready.Models.Models;

public class ThreadHistory
{
    public int Id { get; set; }
    public string Type { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime Timestamp { get; set; }
    public int ThreadId { get; set; }
    public ThreadWorkItem Thread { get; set; } = null!;
}