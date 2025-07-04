using System.ComponentModel.DataAnnotations;

namespace ECommerce.Auth.Api.DTOs;

/// <summary>
/// Data required when a user wants to log in
/// Think of this as a "login form" 
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// User's email address
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's password
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
} 