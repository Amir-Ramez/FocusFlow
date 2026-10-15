namespace FocusFlow.Api.Models {

    public enum TaskPriority {
        Low,
        Normal,
        High,
        Urgent
    }

    public enum TaskStatus {
        Planned,
        Completed
    }

    public class TaskItem {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long? GoalId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DurationMinutes { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Normal;
        public TaskStatus Status { get; set; } = TaskStatus.Planned;
        public string? Category { get; set; }
        public DateTimeOffset? Deadline { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        public User User { get; set; } = null!;
        public Goal? Goal { get; set; }
    }
}
