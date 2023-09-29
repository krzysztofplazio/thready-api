namespace Thready.Models.Models;

public class Priority
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ThreadWorkItem ThreadWorkItem { get; set; } = null!;
}