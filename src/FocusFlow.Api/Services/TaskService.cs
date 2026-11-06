using FocusFlow.Api.Data;
using FocusFlow.Api.DTOs.TaskDTOs;
using FocusFlow.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Services {
    public interface ITaskService {
        Task<List<TaskResponse>> GetTasksAsync(long userId);
        Task<TaskResponse?> GetTaskByIdAsync(long id, long userId);
        Task<TaskResponse?> CreateTaskAsync(CreateTaskDto request, long userId);
        Task<TaskResponse?> UpdateTaskAsync(long id, UpdateTaskDto request, long userId);
        Task<bool> DeleteTaskAsync(long id, long userId);
    }

    public class TaskService : ITaskService {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context) {
            _context = context;
        }

        public async Task<List<TaskResponse>> GetTasksAsync(
            long userId) {
            return await _context.Tasks
                .AsNoTracking()
                .Where(t => t.UserId == userId)
                .Select(t => new TaskResponse {
                    Id = t.Id,
                    UserId = t.UserId,
                    GoalId = t.GoalId,
                    Title = t.Title,
                    Description = t.Description,
                    DurationMinutes = t.DurationMinutes,
                    Priority = t.Priority,
                    Status = t.Status,
                    Category = t.Category,
                    Deadline = t.Deadline,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<TaskResponse?> GetTaskByIdAsync(
            long id,
            long userId) {
            return await _context.Tasks
                .AsNoTracking()
                .Where(t =>
                    t.Id == id &&
                    t.UserId == userId)
                .Select(t => new TaskResponse {
                    Id = t.Id,
                    UserId = t.UserId,
                    GoalId = t.GoalId,
                    Title = t.Title,
                    Description = t.Description,
                    DurationMinutes = t.DurationMinutes,
                    Priority = t.Priority,
                    Status = t.Status,
                    Category = t.Category,
                    Deadline = t.Deadline,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt
                })
                .FirstOrDefaultAsync();
        }

        public async Task<TaskResponse?> CreateTaskAsync(
            CreateTaskDto request,
            long userId) {
            var userExists = await _context.Users
                .AnyAsync(u => u.Id == userId);

            if (!userExists)
                return null;

            if (request.GoalId.HasValue) {
                var goalExists = await _context.Goals
                    .AnyAsync(g =>
                        g.Id == request.GoalId.Value &&
                        g.UserId == userId);

                if (!goalExists)
                    return null;
            }

            var now = DateTimeOffset.UtcNow;

            var task = new TaskItem {
                UserId = userId,
                GoalId = request.GoalId,
                Title = request.Title.Trim(),
                Description = request.Description?.Trim(),
                DurationMinutes = request.DurationMinutes,
                Priority = request.Priority,
                Status = TaskItemStatus.Planned,
                Category = request.Category?.Trim(),
                Deadline = request.Deadline,
                CreatedAt = now,
                UpdatedAt = now
            };

            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return await GetTaskByIdAsync(task.Id, userId);
        }

        public async Task<TaskResponse?> UpdateTaskAsync(
            long id,
            UpdateTaskDto request,
            long userId) {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t =>
                    t.Id == id &&
                    t.UserId == userId);

            if (task is null)
                return null;

            if (request.GoalId.HasValue) {
                var goalExists = await _context.Goals
                    .AnyAsync(g =>
                        g.Id == request.GoalId.Value &&
                        g.UserId == userId);

                if (!goalExists)
                    return null;
            }

            task.GoalId = request.GoalId;
            task.Title = request.Title.Trim();
            task.Description = request.Description?.Trim();
            task.DurationMinutes = request.DurationMinutes;
            task.Priority = request.Priority;
            task.Status = request.Status;
            task.Category = request.Category?.Trim();
            task.Deadline = request.Deadline;
            task.UpdatedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();

            return await GetTaskByIdAsync(task.Id, userId);
        }

        public async Task<bool> DeleteTaskAsync(
            long id,
            long userId) {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t =>
                    t.Id == id &&
                    t.UserId == userId);

            if (task is null)
                return false;

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
