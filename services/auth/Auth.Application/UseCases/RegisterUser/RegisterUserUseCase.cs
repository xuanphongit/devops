using Auth.Application.DTOs;
using Auth.Domain.Entities;
using Auth.Domain.Interfaces;

namespace Auth.Application.UseCases.RegisterUser;

/// <summary>
/// Register user use case implementation
/// </summary>
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public RegisterUserUseCase(
        IUserRepository userRepository,
        IPasswordService passwordService,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto?> ExecuteAsync(RegisterRequestDto request)
    {
        // Check if email already exists
        if (await _userRepository.EmailExistsAsync(request.Email))
        {
            return null; // Email already registered
        }

        // Hash password
        var passwordHash = _passwordService.HashPassword(request.Password);

        // Create user entity
        var user = User.Create(
            request.Email,
            request.FirstName,
            request.LastName,
            passwordHash);

        // Save to repository
        var savedUser = await _userRepository.AddAsync(user);

        // Generate tokens and return response
        return CreateAuthResponse(savedUser);
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