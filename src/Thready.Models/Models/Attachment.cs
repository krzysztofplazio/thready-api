namespace Thready.Models.Models;

public class Attachment
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public byte[] Content { get; set; } = Array.Empty<byte>();
    public int ThreadId { get; set; }
    public ThreadWorkItem ThreadWorkItem { get; set; } = null!;
}