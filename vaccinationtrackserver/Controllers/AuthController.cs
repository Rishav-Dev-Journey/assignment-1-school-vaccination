namespace vaccinationtrackserver.Controllers
{
    // Using statements: Import necessary namespaces
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using vaccinationtrackserver.Data;
    using vaccinationtrackserver.DTOs;
    using vaccinationtrackserver.Models;

    // Route: Defines the base URL for this controller (e.g., /api/auth)
    [Route("api/[controller]")]
    // ApiController: Enables API-specific features like model validation
    [ApiController]
    public class AuthController : ControllerBase
    {
        // _context: Stores the DbContext for database access
        private readonly ApplicationDbContext _context;

        // Constructor: Injects ApplicationDbContext via dependency injection
        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/auth/login
        // Handles coordinator login with simulated authentication
        [HttpPost("login")]
        public async System.Threading.Tasks.Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            // Validate input: Check if loginDTO is null or has empty fields
            if (loginDTO == null || string.IsNullOrEmpty(loginDTO.Username) || string.IsNullOrEmpty(loginDTO.Password))
            {
                // Return 400 Bad Request with error message
                return BadRequest("Invalid login request.");
            }

            // Query database: Find user with matching username and password
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDTO.Username && u.PasswordHash == loginDTO.Password);

            // Check if user exists
            if (user == null)
            {
                // Return 401 Unauthorized for invalid credentials
                return Unauthorized("Invalid credentials.");
            }

            // Return 200 OK with simulated token and role
            // Assignment allows hardcoded/simulated auth, so no real JWT is used
            return Ok(new { Token = "simulated-jwt-token", Role = user.Role });
        }
    }
}