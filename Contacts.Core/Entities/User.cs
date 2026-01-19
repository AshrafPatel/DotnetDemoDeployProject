using Contacts.Core.Enums;
using Contacts.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Contacts.Core.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; set; }

        private User(Guid id, string name, string email, string passwordHash, UserRole role, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = createdAt;
        }

        public static User Create(
            string name, string email, string passwordHash, UserRole role = UserRole.User
        )
        {
            ValidateName(name);
            ValidateEmail(email);
            ValidatePasswordHash(passwordHash);

            return new User(
                Guid.NewGuid(),
                name,
                email,
                passwordHash,
                role,
                DateTime.UtcNow
            );
        }

        public bool IsAdmin() => Role == UserRole.Admin

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name is required");

            if (name.Length < 2)
                throw new DomainException("Name must be at least 2 characters");

            if (name.Length > 100)
                throw new DomainException("Name cannot exceed 100 characters");
        }

        private static void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email is required");

            if (!email.Contains("@"))
                throw new DomainException("Invalid email format");

            if (email.Length > 255)
                throw new DomainException("Email cannot exceed 255 characters");
        }

        private static void ValidatePasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("Password hash is required");
        }


        private User() { }
    }
}
