using FocusFlow.Api.Data;
using FocusFlow.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Controllers {
    [Route("goals")]
    [ApiController]
    public class GoalsController : ControllerBase {
        private readonly AppDbContext _context;

        public GoalsController(AppDbContext context) {
            _context = context;
        }

        // POST /goals
        [HttpPost]
        public async Task<IActionResult> CreateGoal([FromBody] Goal goal) {
            if (string.IsNullOrWhiteSpace(goal.Title)) {
                return BadRequest(new { message = "Goal title is required." });
            }

            goal.CreatedAt = DateTimeOffset.UtcNow;
            goal.UpdatedAt = DateTimeOffset.UtcNow;

            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGoals), new { id = goal.Id }, goal);
        }

        // GET /goals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goal>>> GetGoals() {
            var goals = await _context.Goals
                .Include(g => g.Tasks)
                .ToListAsync();

            return Ok(goals);
        }

        // PUT /goals/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGoal(long id, [FromBody] Goal updatedGoal) {
            var goal = await _context.Goals.FindAsync(id);

            if (goal == null) {
                return NotFound(new { message = "Goal not found." });
            }

            goal.Title = updatedGoal.Title ?? goal.Title;
            goal.Description = updatedGoal.Description ?? goal.Description;
            goal.Status = updatedGoal.Status;
            goal.Deadline = updatedGoal.Deadline;
            goal.UpdatedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(goal);
        }
    }
}
