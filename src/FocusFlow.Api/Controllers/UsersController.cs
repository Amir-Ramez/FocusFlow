using FocusFlow.Api.DTOs.UserDTOs;
using FocusFlow.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FocusFlow.Api.Controllers {
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] CreateUserDto dto) {
            try {
                var result = await _userService.CreateUserAsync(dto);
                return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex) {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<UserResponseDto>> GetUserById([FromRoute] long id) {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) {
                return NotFound(new { message = "User not found" });
            }

            return Ok(user);
        }
    }
}
