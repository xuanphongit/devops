using Microsoft.EntityFrameworkCore;
using ECommerce.Auth.Api.Data;
using ECommerce.Auth.Api.DTOs;
using ECommerce.Auth.Api.Models;

namespace ECommerce.Auth.Api.Services;

/// <summary>
/// Main authentication service implementation
/// Think of this as the "security manager" that handles all user authentication
/// </summary>
public class AuthService : IAuthService
{
    private readonly AuthDbContext _context;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthService(
        AuthDbContext context,
        IPasswordService passwordService,
        ITokenService tokenService,
        IConfiguration configuration)
    {
        _context = context;
        _passwordService = passwordService;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    /// <summary>
    /// Register a new user account
    /// </summary>
    public async Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto request)
    {
        // Check if email already exists
        if (await EmailExistsAsync(request.Email))
        {
            return null; // Email already registered
        }

        // Create new user
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email.ToLowerInvariant().Trim(),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            PasswordHash = _passwordService.HashPassword(request.Password),
            Role = "Customer", // Default role
            IsActive = true,
            IsEmailVerified = false, // In a real app, you'd send verification email
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Save to database
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Generate tokens and return response
        return CreateAuthResponse(user);
    }

    /// <summary>
    /// Login an existing user
    /// </summary>
    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
    {
        // Find user by email
        var user = await GetUserByEmailAsync(request.Email);
        if (user == null || !user.IsActive)
        {
            return null; // User not found or inactive
        }

        // Verify password
        if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash))
        {
            return null; // Wrong password
        }

        // Update last login (optional)
        user.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Generate tokens and return response
        return CreateAuthResponse(user);
    }

    /// <summary>
    /// Get user by email address
    /// </summary>
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant().Trim());
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    /// <summary>
    /// Check if email is already registered
    /// </summary>
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users
            .AnyAsync(u => u.Email == email.ToLowerInvariant().Trim());
    }

    /// <summary>
    /// Create authentication response with tokens
    /// Private helper method to build the response
    /// </summary>
    private AuthResponseDto CreateAuthResponse(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var token = _tokenService.GenerateJwtToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var expirationMinutes = int.Parse(jwtSettings["ExpirationInMinutes"]);

        return new AuthResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes),
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                Role = user.Role,
                IsEmailVerified = user.IsEmailVerified
            }
        };
    }
} 