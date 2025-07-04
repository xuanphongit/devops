using ECommerce.Auth.Api.DTOs;
using ECommerce.Auth.Api.Models;

namespace ECommerce.Auth.Api.Services;

/// <summary>
/// Interface for authentication operations
/// Think of this as the "main security desk" that handles all authentication
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Register a new user account
    /// </summary>
    /// <param name="request">Registration details</param>
    /// <returns>Authentication response with token if successful</returns>
    Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto request);

    /// <summary>
    /// Login an existing user
    /// </summary>
    /// <param name="request">Login credentials</param>
    /// <returns>Authentication response with token if successful</returns>
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);

    /// <summary>
    /// Get user by email address
    /// </summary>
    /// <param name="email">User's email</param>
    /// <returns>User if found, null otherwise</returns>
    Task<User?> GetUserByEmailAsync(string email);

    /// <summary>
    /// Get user by ID
    /// </summary>
    /// <param name="userId">User's unique ID</param>
    /// <returns>User if found, null otherwise</returns>
    Task<User?> GetUserByIdAsync(Guid userId);

    /// <summary>
    /// Check if email is already registered
    /// </summary>
    /// <param name="email">Email to check</param>
    /// <returns>True if email exists, false otherwise</returns>
    Task<bool> EmailExistsAsync(string email);
} 