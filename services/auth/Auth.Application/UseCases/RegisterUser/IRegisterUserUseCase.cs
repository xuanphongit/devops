using Auth.Application.DTOs;

namespace Auth.Application.UseCases.RegisterUser;

/// <summary>
/// Register user use case interface
/// </summary>
public interface IRegisterUserUseCase
{
    /// <summary>
    /// Execute the register user use case
    /// </summary>
    Task<AuthResponseDto?> ExecuteAsync(RegisterRequestDto request);
} 