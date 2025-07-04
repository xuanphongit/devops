using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ECommerce.Auth.Api.DTOs;
using ECommerce.Auth.Api.Services;
using System.Security.Claims;

namespace ECommerce.Auth.Api.Controllers;

/// <summary>
/// Authentication controller - this is the "front desk" for all authentication requests
/// Think of this as the reception that handles visitor requests
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Register a new user account
    /// POST: api/auth/register
    /// </summary>
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterRequestDto request)
    {
        try
        {
            // Validate the incoming data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Try to register the user
            var result = await _authService.RegisterAsync(request);

            if (result == null)
            {
                return BadRequest(new { message = "Email already exists or registration failed" });
            }

            _logger.LogInformation("User registered successfully: {Email}", request.Email);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during user registration: {Email}", request.Email);
            return StatusCode(500, new { message = "An error occurred during registration" });
        }
    }

    /// <summary>
    /// Login an existing user
    /// POST: api/auth/login
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            // Validate the incoming data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Try to login the user
            var result = await _authService.LoginAsync(request);

            if (result == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            _logger.LogInformation("User logged in successfully: {Email}", request.Email);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during user login: {Email}", request.Email);
            return StatusCode(500, new { message = "An error occurred during login" });
        }
    }

    /// <summary>
    /// Get current user profile (requires authentication)
    /// GET: api/auth/profile
    /// </summary>
    [HttpGet("profile")]
    [Authorize] // This requires a valid JWT token
    public async Task<ActionResult<UserDto>> GetProfile()
    {
        try
        {
            // Get user ID from the JWT token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            // Get user from database
            var user = await _authService.GetUserByIdAsync(userId);
            
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Return user profile (without sensitive information)
            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                Role = user.Role,
                IsEmailVerified = user.IsEmailVerified
            };

            return Ok(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user profile");
            return StatusCode(500, new { message = "An error occurred getting profile" });
        }
    }

    /// <summary>
    /// Check if email is available
    /// GET: api/auth/check-email?email=test@example.com
    /// </summary>
    [HttpGet("check-email")]
    public async Task<ActionResult<object>> CheckEmail([FromQuery] string email)
    {
        try
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { message = "Email is required" });
            }

            var exists = await _authService.EmailExistsAsync(email);
            
            return Ok(new { 
                email = email, 
                available = !exists,
                message = exists ? "Email is already taken" : "Email is available"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking email availability: {Email}", email);
            return StatusCode(500, new { message = "An error occurred checking email" });
        }
    }
} 