using Auth.Domain.Entities;

namespace Auth.Domain.Interfaces;

/// <summary>
/// Repository interface for User entity
/// This defines the contract for data access without specifying implementation details
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Get user by ID
    /// </summary>
    Task<User?> GetByIdAsync(Guid id);

    /// <summary>
    /// Get user by email address
    /// </summary>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// Check if email exists
    /// </summary>
    Task<bool> EmailExistsAsync(string email);

    /// <summary>
    /// Add a new user
    /// </summary>
    Task<User> AddAsync(User user);

    /// <summary>
    /// Update an existing user
    /// </summary>
    Task<User> UpdateAsync(User user);

    /// <summary>
    /// Delete a user
    /// </summary>
    Task DeleteAsync(Guid id);
} 