using Microsoft.EntityFrameworkCore;
using ECommerce.Auth.Api.Models;

namespace ECommerce.Auth.Api.Data;

/// <summary>
/// Database context - think of this as your "database manager"
/// It handles all communication between our code and the PostgreSQL database
/// </summary>
public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Users table in the database
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Configure the database schema (like designing the table structure)
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            // Set the table name
            entity.ToTable("Users");

            // Configure primary key
            entity.HasKey(u => u.Id);

            // Configure unique constraints
            entity.HasIndex(u => u.Email).IsUnique();

            // Configure properties
            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            entity.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(u => u.PasswordHash)
                .IsRequired();

            entity.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Customer");

            entity.Property(u => u.IsActive)
                .HasDefaultValue(true);

            entity.Property(u => u.IsEmailVerified)
                .HasDefaultValue(false);

            entity.Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(u => u.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
} 