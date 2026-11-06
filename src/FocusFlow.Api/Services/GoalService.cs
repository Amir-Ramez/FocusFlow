using FocusFlow.Api.Data;
using FocusFlow.Api.DTOs;
using FocusFlow.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Services {
    public interface IGoalService {
        Task<IReadOnlyList<GoalResponse>> GetGoalsAsync();
        Task<GoalResponse?> GetGoalByIdAsync(long id);
        Task<GoalResponse> CreateGoalAsync(CreateGoalRequest request);
        Task<GoalResponse?> UpdateGoalAsync(long id, UpdateGoalRequest request);
        Task<bool> DeleteGoalAsync(long id);
    }

    public class GoalService : IGoalService {
        private readonly AppDbContext _context;

        // Temporary development user until authentication is implemented.
        private readonly long _devUserId;

        public GoalService(AppDbContext context, IConfiguration configuration) {
            _context = context;
            _devUserId = configuration.GetValue<long?>("DevUser:UserId") ?? 1;
        }

        public async Task<IReadOnlyList<GoalResponse>> GetGoalsAsync() {
            var goals = await _context.Goals
                .AsNoTracking()
                .Where(g => g.UserId == _devUserId)
                .OrderByDescending(g => g.CreatedAt)
                .ThenBy(g => g.Id)
                .ToListAsync();

            return goals.Select(MapToResponse).ToList();
        }

        public async Task<GoalResponse?> GetGoalByIdAsync(long id) {
            var goal = await _context.Goals
                .AsNoTracking()
                .FirstOrDefaultAsync(g =>
                    g.Id == id && g.UserId == _devUserId);

            return goal is null ? null : MapToResponse(goal);
        }

        public async Task<GoalResponse> CreateGoalAsync(CreateGoalRequest request) {
            if (string.IsNullOrWhiteSpace(request.Title)) {
                throw new ArgumentException("Goal title is required.");
            }

            var userExists = await _context.Users
                .AnyAsync(u => u.Id == _devUserId);

            if (!userExists) {
                throw new InvalidOperationException(
                    $"The development user with id {_devUserId} does not exist. " +
                    "Create a user or set DevUser:UserId in configuration.");
            }

            var now = DateTimeOffset.UtcNow;

            var goal = new Goal {
                Title = request.Title.Trim(),
                Description = request.Description?.Trim(),
                Status = request.Status ?? GoalStatus.Active,
                Deadline = request.Deadline,
                UserId = _devUserId,
                CreatedAt = now,
                UpdatedAt = now
            };

            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();

            return MapToResponse(goal);
        }

        public async Task<GoalResponse?> UpdateGoalAsync(
            long id,
            UpdateGoalRequest request) {
            var goal = await _context.Goals
                .FirstOrDefaultAsync(g =>
                    g.Id == id && g.UserId == _devUserId);

            if (goal is null) {
                return null;
            }

            if (request.Title is not null) {
                if (string.IsNullOrWhiteSpace(request.Title)) {
                    throw new ArgumentException(
                        "Goal title cannot be empty.");
                }

                goal.Title = request.Title.Trim();
            }

            if (request.Description is not null) {
                goal.Description = request.Description.Trim();
            }

            if (request.Status.HasValue) {
                goal.Status = request.Status.Value;
            }

            if (request.Deadline.HasValue) {
                goal.Deadline = request.Deadline.Value;
            }

            goal.UpdatedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();

            return MapToResponse(goal);
        }

        public async Task<bool> DeleteGoalAsync(long id) {
            var goal = await _context.Goals
                .FirstOrDefaultAsync(g =>
                    g.Id == id && g.UserId == _devUserId);

            if (goal is null) {
                return false;
            }

            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();

            return true;
        }

        private static GoalResponse MapToResponse(Goal goal) {
            return new GoalResponse {
                Id = goal.Id,
                Title = goal.Title,
                Description = goal.Description,
                Status = goal.Status,
                Deadline = goal.Deadline,
                CreatedAt = goal.CreatedAt,
                UpdatedAt = goal.UpdatedAt
            };
        }
    }
}
