using FocusFlow.Api.DTOs;
using FocusFlow.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FocusFlow.Api.Controllers {
    [ApiController]
    [Route("goals")]
    public class GoalsController : ControllerBase {
        private readonly IGoalService _goalService;

        public GoalsController(IGoalService goalService) {
            _goalService = goalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoalResponse>>> GetGoals() {
            var goals = await _goalService.GetGoalsAsync();
            return Ok(goals);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<GoalResponse>> GetGoalById(long id) {
            var goal = await _goalService.GetGoalByIdAsync(id);

            if (goal is null) {
                return NotFound(new { message = "Goal not found." });
            }

            return Ok(goal);
        }

        [HttpPost]
        public async Task<ActionResult<GoalResponse>> CreateGoal([FromBody] CreateGoalRequest request) {
            try {
                var createdGoal = await _goalService.CreateGoalAsync(request);

                return CreatedAtAction(
                    nameof(GetGoalById),
                    new { id = createdGoal.Id },
                    createdGoal);
            }
            catch (ArgumentException ex) {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<GoalResponse>> UpdateGoal(long id, [FromBody] UpdateGoalRequest request) {
            try {
                var updatedGoal = await _goalService.UpdateGoalAsync(id, request);

                if (updatedGoal is null) {
                    return NotFound(new { message = "Goal not found." });
                }

                return Ok(updatedGoal);
            }
            catch (ArgumentException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteGoal(long id) {
            var deleted = await _goalService.DeleteGoalAsync(id);

            if (!deleted) {
                return NotFound(new { message = "Goal not found." });
            }

            return NoContent();
        }
    }
}
