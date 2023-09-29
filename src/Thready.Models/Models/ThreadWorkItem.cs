namespace Thready.Models.Models;

public class ThreadWorkItem
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int StateId { get; set; }
    public State State { get; set; } = null!;
    public int PriorityId { get; set; }
    public Priority Priority { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IEnumerable<ThreadHistory> ThreadHistories { get; set; } = Enumerable.Empty<ThreadHistory>();
    public DateTime CreateDate { get; set; }
    public DateTime ModificateDate { get; set; }
    public int CreatedByUserId { get; set; }
    public User CreatedBy { get; set; } = null!;
    public int OwnedByUserId { get; set; }
    public User OwnedBy { get; set; } = null!;
    public DateTime? DueDate { get; set; }
    public IEnumerable<Comment> Comments { get; set; } = Enumerable.Empty<Comment>();
    public IEnumerable<Attachment> Attachments { get; set; } = Enumerable.Empty<Attachment>();
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}