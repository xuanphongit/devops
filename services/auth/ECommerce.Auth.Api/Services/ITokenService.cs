using ECommerce.Auth.Api.Models;

namespace ECommerce.Auth.Api.Services;

/// <summary>
/// Interface for JWT token operations
/// Think of this as a "digital ID card maker"
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generate a JWT token for a user (like creating an ID card)
    /// </summary>
    /// <param name="user">The user to create a token for</param>
    /// <returns>JWT token string</returns>
    string GenerateJwtToken(User user);

    /// <summary>
    /// Generate a refresh token (for getting new JWT tokens)
    /// </summary>
    /// <returns>Refresh token string</returns>
    string GenerateRefreshToken();

    /// <summary>
    /// Get user ID from a JWT token
    /// </summary>
    /// <param name="token">JWT token</param>
    /// <returns>User ID if token is valid, null otherwise</returns>
    Guid? GetUserIdFromToken(string token);
} 