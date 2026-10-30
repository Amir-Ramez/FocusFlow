using FocusFlow.Api.Data;
using FocusFlow.Api.DTOs.UserDTOs;
using FocusFlow.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace FocusFlow.Api.Services {
    public interface IUserService {
        Task<UserResponseDto> CreateUserAsync(CreateUserDto dto);
        Task<UserResponseDto?> GetUserByIdAsync(long id);
    }

    public class UserService : IUserService {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context) {
            _context = context;
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto) {
            var name = dto.Name.Trim();
            var email = dto.Email.Trim().ToLowerInvariant();
            var timezone = dto.TimeZone.Trim();

            var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (emailExists) {
                throw new InvalidOperationException("Email is already in use.");
            }

            var user = new User {
                Name = dto.Name,
                Email = dto.Email,
                TimeZone = dto.TimeZone,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponseDto {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                TimeZone = user.TimeZone
            };
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(long id) {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserResponseDto {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                TimeZone = user.TimeZone
            };
        }
    }
}
