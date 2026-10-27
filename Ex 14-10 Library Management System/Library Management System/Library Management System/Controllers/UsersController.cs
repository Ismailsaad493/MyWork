using Library_Management_System.Entities;
using Library_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _userService.RegisterAsync(dto);
            if (result == null) return BadRequest(new { Message = "Username already exists." });
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _userService.LoginAsync(dto);
            if (result == null) return Unauthorized(new { Message = "Invalid credentials." });
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails(int id)
        {
            var user = await _userService.GetUserDetailsAsync(id);
            if (user == null) return NotFound(new { Message = "User not found." });
            return Ok(user);
        }
        // PUT: api/Users/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, dto);

            if (updatedUser == null)
                return NotFound(new { Message = "User not found." });

            return Ok(new { Message = "User updated successfully.", User = updatedUser });
        }

    }
}