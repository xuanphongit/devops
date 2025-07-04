using System.ComponentModel.DataAnnotations;

namespace ECommerce.Auth.Api.Models;

/// <summary>
/// Represents a user in our e-commerce platform
/// Think of this as a "digital profile card" for each customer
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for each user (like a social security number)
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User's email address - used for login
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's first name
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// User's last name
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Hashed password (we never store actual passwords!)
    /// </summary>
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// User's role in the system (Customer, Admin, etc.)
    /// </summary>
    [Required]
    public string Role { get; set; } = "Customer";

    /// <summary>
    /// Is the user's email verified?
    /// </summary>
    public bool IsEmailVerified { get; set; } = false;

    /// <summary>
    /// Is the user account active?
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// When the user account was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// When the user account was last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// User's full name (computed property)
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
} 