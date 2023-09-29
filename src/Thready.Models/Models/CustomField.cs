namespace Thready.Models.Models;

public class CustomField
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool IsRequired { get; set; }
}