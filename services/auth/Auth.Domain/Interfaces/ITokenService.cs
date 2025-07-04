using Auth.Domain.Entities;

namespace Auth.Domain.Interfaces;

/// <summary>
/// Token service interface for domain operations
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generate JWT token for user
    /// </summary>
    string GenerateJwtToken(User user);

    /// <summary>
    /// Generate refresh token
    /// </summary>
    string GenerateRefreshToken();

    /// <summary>
    /// Get user ID from token
    /// </summary>
    Guid? GetUserIdFromToken(string token);
} 