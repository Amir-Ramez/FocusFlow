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
        [EnumDataType(typeof(TaskPriority))]
        public TaskPriority Priority { get; set; } = TaskPriority.Normal;
        [EnumDataType(typeof(FocusFlow.Api.Models.TaskStatus))]
        public FocusFlow.Api.Models.TaskStatus Status { get; set; } = FocusFlow.Api.Models.TaskStatus.Planned;
        public string? Category { get; set; }
        public DateTimeOffset? Deadline { get; set; }
        public long? GoalId { get; set; }
    }
}
