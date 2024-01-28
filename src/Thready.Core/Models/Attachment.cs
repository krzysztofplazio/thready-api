namespace Thready.Core.Models;

public class Attachment
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public byte[] Content { get; set; } = Array.Empty<byte>();
    public int ThreadId { get; set; }
    public ThreadWorkItem Thread { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}