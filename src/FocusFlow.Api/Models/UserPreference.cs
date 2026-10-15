
namespace FocusFlow.Api.Models;

public class UserPreference {
    public long Id { get; set; }
    public long UserId { get; set; }
    public TimeSpan? PreferredStartTime { get; set; }
    public TimeSpan? PreferredEndTime { get; set; }
    public int FocusDurationMinutes { get; set; } = 25;
    public int BreakDurationMinutes { get; set; } = 5;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public User User { get; set; } = null!;
}
