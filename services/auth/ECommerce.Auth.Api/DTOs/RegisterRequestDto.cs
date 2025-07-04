using System.ComponentModel.DataAnnotations;

namespace ECommerce.Auth.Api.DTOs;

/// <summary>
/// Data required when a new user wants to register
/// Think of this as a "registration form" with validation rules
/// </summary>
public class RegisterRequestDto
{
    /// <summary>
    /// User's email address (must be valid email format)
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's first name (required, max 50 characters)
    /// </summary>
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// User's last name (required, max 50 characters)
    /// </summary>
    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// User's password (minimum 6 characters for security)
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Password confirmation (must match password)
    /// </summary>
    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
} 