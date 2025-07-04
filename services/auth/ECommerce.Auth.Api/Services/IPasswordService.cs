namespace ECommerce.Auth.Api.Services;

/// <summary>
/// Interface for password-related operations
/// Think of this as a "contract" that defines what password operations we need
/// </summary>
public interface IPasswordService
{
    /// <summary>
    /// Hash a plain text password for secure storage
    /// (Never store actual passwords - always hash them!)
    /// </summary>
    /// <param name="password">The plain text password</param>
    /// <returns>Hashed password string</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verify if a plain text password matches the stored hash
    /// </summary>
    /// <param name="password">Plain text password to check</param>
    /// <param name="hash">Stored password hash</param>
    /// <returns>True if password matches, false otherwise</returns>
    bool VerifyPassword(string password, string hash);
} 