namespace FocusFlow.Api.DTOs.UserDTOs {
    public class UserResponseDto {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TimeZone { get; set; } = string.Empty;
    }
}
