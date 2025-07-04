using System.ComponentModel.DataAnnotations;

namespace Auth.Application.DTOs;

/// <summary>
/// Login request DTO
/// </summary>
public class LoginRequestDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
} 