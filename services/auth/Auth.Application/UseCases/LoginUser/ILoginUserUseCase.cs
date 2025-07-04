using Auth.Application.DTOs;

namespace Auth.Application.UseCases.LoginUser;

/// <summary>
/// Login user use case interface
/// </summary>
public interface ILoginUserUseCase
{
    /// <summary>
    /// Execute the login user use case
    /// </summary>
    Task<AuthResponseDto?> ExecuteAsync(LoginRequestDto request);
} 