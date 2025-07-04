using System.ComponentModel.DataAnnotations;

namespace Auth.Application.DTOs;

/// <summary>
/// Registration request DTO
/// </summary>
public class RegisterRequestDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "First name is required")]
    [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
} 