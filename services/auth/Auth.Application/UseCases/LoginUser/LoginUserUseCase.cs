using Auth.Application.DTOs;
using Auth.Domain.Entities;
using Auth.Domain.Interfaces;

namespace Auth.Application.UseCases.LoginUser;

/// <summary>
/// Login user use case implementation
/// </summary>
public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public LoginUserUseCase(
        IUserRepository userRepository,
        IPasswordService passwordService,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto?> ExecuteAsync(LoginRequestDto request)
    {
        // Find user by email
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !user.IsActive)
        {
            return null; // User not found or inactive
        }

        // Verify password
        if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash))
        {
            return null; // Wrong password
        }

        // Update last login
        user.UpdateLastLogin();
        await _userRepository.UpdateAsync(user);

        // Generate tokens and return response
        return CreateAuthResponse(user);
    }

    private AuthResponseDto CreateAuthResponse(User user)
    {
        var token = _tokenService.GenerateJwtToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        return new AuthResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15), // TODO: Get from config
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                Role = user.Role,
                IsEmailVerified = user.IsEmailVerified
            }
        };
    }
} 