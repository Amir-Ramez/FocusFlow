using FocusFlow.Api.Models;

namespace FocusFlow.Api.DTOs.TaskDTOs {
    public class TaskResponse {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long? GoalId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DurationMinutes { get; set; }

        public TaskItemPriority Priority { get; set; }

        public TaskItemStatus Status { get; set; }

        public string? Category { get; set; }

        public DateTimeOffset? Deadline { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
