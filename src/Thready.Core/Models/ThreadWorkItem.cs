namespace Thready.Core.Models;

public class ThreadWorkItem
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int StateId { get; set; }
    public State State { get; set; } = null!;
    public int PriorityId { get; set; }
    public Priority Priority { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<ThreadHistory> ThreadHistories { get; set; } = [];
    public DateTime CreateDate { get; set; }
    public DateTime ModificateDate { get; set; }
    public int CreatedByUserId { get; set; }
    public User CreatedBy { get; set; } = null!;
    public int OwnedByUserId { get; set; }
    public User OwnedBy { get; set; } = null!;
    public DateTime? DueDate { get; set; }
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<Attachment> Attachments { get; set; } = [];
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public ICollection<CustomValue> CustomValues { get; set; } = [];
}