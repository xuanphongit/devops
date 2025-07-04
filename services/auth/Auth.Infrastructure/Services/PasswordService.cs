using BCrypt.Net;
using Auth.Domain.Interfaces;

namespace Auth.Infrastructure.Services;

/// <summary>
/// Password service implementation
/// </summary>
public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }

    public bool VerifyPassword(string password, string hash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        catch
        {
            return false;
        }
    }
} 