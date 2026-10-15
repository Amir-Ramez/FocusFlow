namespace FocusFlow.Api.Models {
    public class User {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TimeZone { get; set; } = "UTC";
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        public ICollection<Goal> Goals { get; set; } = new List<Goal>();
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public UserPreference? Preference { get; set; }
    }
}
