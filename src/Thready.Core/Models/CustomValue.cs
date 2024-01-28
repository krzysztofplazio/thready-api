namespace Thready.Core.Models;

public class CustomValue
{
    public int Id { get; set; }
    public string FieldId { get; set; } = null!;
    public CustomField CustomField { get; set; } = null!;
    public string Value { get; set; } = null!;
    public int ThreadId { get; set; }
    public ThreadWorkItem Thread { get; set; } = null!;
    public DateTime CreateDate { get; set; }
}