namespace Auth.Domain.Entities;

/// <summary>
/// User entity - represents a user in our domain
/// This is the core business entity with no dependencies on external concerns
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for the user
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// User's email address (unique identifier)
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// User's first name
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// User's last name
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Hashed password (never store plain text passwords)
    /// </summary>
    public string PasswordHash { get; private set; }

    /// <summary>
    /// User's role in the system
    /// </summary>
    public string Role { get; private set; }

    /// <summary>
    /// Whether the user's email is verified
    /// </summary>
    public bool IsEmailVerified { get; private set; }

    /// <summary>
    /// Whether the user account is active
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// When the user was created
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// When the user was last updated
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    /// User's full name (computed property)
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    // Private constructor for EF Core
    private User() { }

    /// <summary>
    /// Create a new user
    /// </summary>
    public static User Create(
        string email,
        string firstName,
        string lastName,
        string passwordHash,
        string role = "Customer")
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = email.ToLowerInvariant().Trim(),
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            PasswordHash = passwordHash,
            Role = role,
            IsEmailVerified = false,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Update user information
    /// </summary>
    public void Update(string firstName, string lastName)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Mark email as verified
    /// </summary>
    public void VerifyEmail()
    {
        IsEmailVerified = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Deactivate user account
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Activate user account
    /// </summary>
    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Update last login time
    /// </summary>
    public void UpdateLastLogin()
    {
        UpdatedAt = DateTime.UtcNow;
    }
} 