namespace FocusFlow.Api.Models {

    public enum TaskItemPriority {
        Low,
        Normal,
        High,
        Urgent
    }

    public enum TaskItemStatus {
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
        public TaskItemPriority Priority { get; set; } = TaskItemPriority.Normal;
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Planned;
        public string? Category { get; set; }
        public DateTimeOffset? Deadline { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        public User User { get; set; } = null!;
        public Goal? Goal { get; set; }
    }
}
