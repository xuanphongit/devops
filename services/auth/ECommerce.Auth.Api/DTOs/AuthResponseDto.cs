namespace ECommerce.Auth.Api.DTOs;

/// <summary>
/// Response sent back after successful login or registration
/// Think of this as the "digital ID card" we give to users
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    /// The JWT token (like a temporary pass card)
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Refresh token for getting new access tokens
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// When the token expires
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// User information
    /// </summary>
    public UserDto User { get; set; } = new();
}

/// <summary>
/// User information to send back (without sensitive data)
/// </summary>
public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsEmailVerified { get; set; }
} 