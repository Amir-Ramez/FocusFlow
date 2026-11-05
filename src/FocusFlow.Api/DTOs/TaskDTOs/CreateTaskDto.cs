using System.ComponentModel.DataAnnotations;
using FocusFlow.Api.Models;

namespace FocusFlow.Api.DTOs.TaskDTOs {
    public class CreateTaskDto {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Range(1, 1440)]
        public int DurationMinutes { get; set; }
        [EnumDataType(typeof(TaskPriority))]
        public TaskPriority Priority { get; set; } = TaskPriority.Normal;
        public string? Category { get; set; }
        public DateTimeOffset? Deadline { get; set; }
        public long? GoalId { get; set; }
    }
}
