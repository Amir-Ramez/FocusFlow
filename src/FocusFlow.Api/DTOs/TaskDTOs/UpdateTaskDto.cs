using System.ComponentModel.DataAnnotations;
using FocusFlow.Api.Models;

namespace FocusFlow.Api.DTOs.TaskDTOs {
    public class UpdateTaskDto {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Range(1, 1440)]
        public int DurationMinutes { get; set; }
        [EnumDataType(typeof(TaskItemPriority))]
        public TaskItemPriority Priority { get; set; } = TaskItemPriority.Normal;
        [EnumDataType(typeof(TaskItemStatus))]
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Planned;
        public string? Category { get; set; }
        public DateTimeOffset? Deadline { get; set; }
        public long? GoalId { get; set; }
    }
}
