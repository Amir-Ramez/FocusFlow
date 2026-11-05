using System.ComponentModel.DataAnnotations;
using FocusFlow.Api.Models;

namespace FocusFlow.Api.DTOs {
    public class CreateGoalRequest {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [EnumDataType(typeof(GoalStatus))]
        public GoalStatus? Status { get; set; }

        public DateTimeOffset? Deadline { get; set; }
    }

    public class UpdateGoalRequest {
        [StringLength(200)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [EnumDataType(typeof(GoalStatus))]
        public GoalStatus? Status { get; set; }

        public DateTimeOffset? Deadline { get; set; }
    }

    public class GoalResponse {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public GoalStatus Status { get; set; }
        public DateTimeOffset? Deadline { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
