namespace FocusFlow.Api.Models;

public enum GoalStatus {
    Active,
    Completed,
    Archived
}

public class Goal {
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public GoalStatus Status { get; set; } = GoalStatus.Active;
    public DateTimeOffset? Deadline { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public User User { get; set; } = null!;
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
