using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class User
    {
        // Primary Key
        public Guid Id { get; private set; }

        // Core Info
        public string Username { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Firstname { get; private set; } = string.Empty;
        public string Middlename { get; private set; } = string.Empty;
        public string Lastname { get; private set; } = string.Empty;

        // Security Info
        public string PasswordHash { get; private set; } = string.Empty;
        public string? ResetToken { get; private set; }
        public DateTime? ResetTokenExpiry { get; private set; }

        // Status & Auditing
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }

        public User(string username, string email, string firstname, string middlename, string lastname, string passwordHash)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            Firstname = firstname;
            Middlename = middlename;
            Lastname = lastname;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateProfile(string firstname, string middlename, string lastname, string email)
        {
            Firstname = firstname;
            Middlename = middlename;
            Lastname = lastname;
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }
        public void SetPassword(string passwordHash)
        {
            PasswordHash = passwordHash;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetResetToken(string token, DateTime expiry)
        {
            ResetToken = token;
            ResetTokenExpiry = expiry;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ClearResetToken()
        {
            ResetToken = null;
            ResetTokenExpiry = null;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }



    }
}
