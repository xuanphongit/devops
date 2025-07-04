using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Auth.Application.DTOs;
using Auth.Application.UseCases.RegisterUser;
using Auth.Application.UseCases.LoginUser;
using Auth.Domain.Interfaces;

namespace Auth.API.Controllers;

/// <summary>
/// Authentication controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IRegisterUserUseCase _registerUserUseCase;
    private readonly ILoginUserUseCase _loginUserUseCase;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IRegisterUserUseCase registerUserUseCase,
        ILoginUserUseCase loginUserUseCase,
        IUserRepository userRepository,
        ILogger<AuthController> logger)
    {
        _registerUserUseCase = registerUserUseCase;
        _loginUserUseCase = loginUserUseCase;
        _userRepository = userRepository;
        _logger = logger;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterRequestDto request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _registerUserUseCase.ExecuteAsync(request);

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
    /// Login user
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _loginUserUseCase.ExecuteAsync(request);

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
    /// Get current user profile
    /// </summary>
    [HttpGet("profile")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetProfile()
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var user = await _userRepository.GetByIdAsync(userId);
            
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

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

            var exists = await _userRepository.EmailExistsAsync(email);
            
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