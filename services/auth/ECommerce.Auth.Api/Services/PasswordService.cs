using BCrypt.Net;

namespace ECommerce.Auth.Api.Services;

/// <summary>
/// Implementation of password service using BCrypt for secure hashing
/// BCrypt is like a "one-way scrambler" - you can't unscramble the password
/// </summary>
public class PasswordService : IPasswordService
{
    /// <summary>
    /// Hash a password using BCrypt (secure and slow on purpose)
    /// </summary>
    public string HashPassword(string password)
    {
        // BCrypt.HashPassword creates a hash that includes:
        // - Salt (random data to make each hash unique)
        // - Hash algorithm
        // - Cost factor (how slow/secure it should be)
        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }

    /// <summary>
    /// Verify if a password matches the stored hash
    /// </summary>
    public bool VerifyPassword(string password, string hash)
    {
        try
        {
            // BCrypt.Verify compares the password with the hash
            // It automatically handles the salt and algorithm details
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        catch
        {
            // If something goes wrong, assume password is wrong
            return false;
        }
    }
} 