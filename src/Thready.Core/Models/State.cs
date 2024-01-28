namespace Thready.Core.Models;

public class State
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ThreadWorkItem ThreadWorkItem { get; set; } = null!;
}