using FocusFlow.Api.DTOs.TaskDTOs;
using FocusFlow.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FocusFlow.Api.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class TasksController : ControllerBase {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService) {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] long userId) {
            var tasks = await _taskService.GetTasksAsync(userId);

            return Ok(tasks);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetTask(
            long id,
            [FromQuery] long userId) {
            var task = await _taskService.GetTaskByIdAsync(id, userId);

            if (task is null)
                return NotFound(new { message = "Task not found." });

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(
            [FromBody] CreateTaskDto request,
            [FromQuery] long userId) {
            var task = await _taskService.CreateTaskAsync(request, userId);

            if (task is null)
                return BadRequest(new {
                    message = "User or goal not found."
                });

            return CreatedAtAction(
                nameof(GetTask),
                new { id = task.Id, userId = task.UserId },
                task
            );
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateTask(
            long id,
            [FromBody] UpdateTaskDto request,
            [FromQuery] long userId) {
            var task = await _taskService.UpdateTaskAsync(
                id,
                request,
                userId);

            if (task is null)
                return NotFound(new {
                    message = "Task or goal not found."
                });

            return Ok(task);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteTask(
            long id,
            [FromQuery] long userId) {
            var deleted = await _taskService.DeleteTaskAsync(id, userId);

            if (!deleted)
                return NotFound(new { message = "Task not found." });

            return NoContent();
        }
    }
}
