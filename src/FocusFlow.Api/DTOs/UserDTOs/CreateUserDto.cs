using System.ComponentModel.DataAnnotations;
namespace FocusFlow.Api.DTOs.UserDTOs {
    public class CreateUserDto {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string TimeZone { get; set; } = "UTC";
    }
}
