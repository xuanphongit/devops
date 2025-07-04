namespace Auth.Domain.Interfaces;

/// <summary>
/// Password service interface for domain operations
/// </summary>
public interface IPasswordService
{
    /// <summary>
    /// Hash a plain text password
    /// </summary>
    string HashPassword(string password);

    /// <summary>
    /// Verify if a password matches the hash
    /// </summary>
    bool VerifyPassword(string password, string hash);
} 